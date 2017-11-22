using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RussiaCube
{
	class Worker
	{
		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="RowCount">竖直格子数</param>
		/// <param name="ColCount">水平格子数</param>
		/// <param name="RectPix">格子像素宽度</param>
		/// <param name="CellBackColor">画板背景色</param>
		/// <param name="CellForeColorArray">方格子前景色</param>
		/// <param name="viewGraphics">活动窗格画笔</param>
		/// <param name="preGraphics">预览窗格画笔</param>
		/// <param name="sleepTime">睡眠时间</param>
		public Worker(int RowCount, int ColCount, int RectPix, Color CellBackColor, Color[] CellForeColorArray,
			Graphics viewGraphics, Graphics preGraphics, int sleepTime, Label score)
		{
			_colCount = ColCount;
			_rowCount = RowCount;
			_cells = new int[_rowCount, _colCount];
			for (int i = 0; i < _rowCount; i++)
			{
				for (int j = 0; j < _colCount; j++)
				{
					_cells[i, j] = -1;
				}
			}
			_rectPic = RectPix;
			_bgColor = CellBackColor;
			_bgbrush = new SolidBrush(CellBackColor);
			_foreColors = CellForeColorArray;
			_forebrush = new Brush[_foreColors.Length];
			for (int i = 0; i < _foreColors.Length; i++)
			{
				_forebrush[i] = new SolidBrush(_foreColors[i]);
			}
			runGraphics = viewGraphics;
			readyGraphics = preGraphics;

			_labelScore = score;
			Score = 0;

			timer = new Timer();
			timer.Interval = sleepTime;
			timer.Tick += new EventHandler(timer_Tick);
		}

		public void InitGame()
		{
			_runningBlock = Block.GetABlock(_foreColors.Length);
			_runningBlock.ColPos = _colCount / 2;
			_runningBlock.RowPos = 1;
			lock (runGraphics)
			{
				runGraphics.Clear(_bgColor);
				for (int i = 0; i < _runningBlock.Cells.Length; i++)
				{
					runGraphics.FillRectangle(
						_forebrush[_runningBlock.ColorIndex],
						(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
						(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
			_readyBlock = Block.GetABlock(_foreColors.Length);
			_readyBlock.ColPos = 2;
			_readyBlock.RowPos = 2;
			lock (readyGraphics)
			{
				readyGraphics.Clear(_bgColor);
				for (int i = 0; i < _readyBlock.Cells.Length; i++)
				{
					readyGraphics.FillRectangle(
						_forebrush[_readyBlock.ColorIndex],
						(_readyBlock.ColPos + _readyBlock.Cells[i].X) * _rectPic + 1,
						(_readyBlock.RowPos + _readyBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
			Score = 0;
		}

		void timer_Tick(object sender, EventArgs e)
		{
			if (MeetBottom())//砖块到底
			{
				CombineBlock();
				_runningBlock = _readyBlock;
				_runningBlock.ColPos = _colCount / 2;
				_runningBlock.RowPos = 1;
				_readyBlock = Block.GetABlock(_foreColors.Length);
				_readyBlock.ColPos = 2;
				_readyBlock.RowPos = 2;
				DrawRunningBlock();
				ReDrawReadyBlock();
			}
			else//砖块未到底，将砖块下移一格
			{
				ClearRunningBlock();
				_runningBlock.RowPos++;
				DrawRunningBlock();
			}
		}

		/// <summary>
		/// 当前砖块是否到底
		/// 即 当前砖块下方是否是游戏区域底端或者是有砖块存在
		/// </summary>
		/// <returns></returns>
		private bool MeetBottom()
		{
			for (int x = 0; x < _runningBlock.Cells.Length; x++)
			{
				if ((_runningBlock.RowPos + _runningBlock.Cells[x].Y >= _rowCount - 1) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[x].Y + 1, _runningBlock.ColPos + _runningBlock.Cells[x].X] != -1))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 砖块左边是否到墙 或者左边有砖块
		/// </summary>
		/// <returns></returns>
		private bool MeetLeft()
		{
			for (int i = 0; i < _runningBlock.Cells.Length; i++)
			{
				if ((_runningBlock.ColPos + _runningBlock.Cells[i].X <= 0) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[i].Y, _runningBlock.ColPos + _runningBlock.Cells[i].X - 1] != -1))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 砖块右边是否到墙 或者右边有砖块
		/// </summary>
		/// <returns></returns>
		private bool MeetRight()
		{
			for (int i = 0; i < _runningBlock.Cells.Length; i++)
			{
				if ((_runningBlock.ColPos + _runningBlock.Cells[i].X + 1 >= _colCount) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[i].Y, _runningBlock.ColPos + _runningBlock.Cells[i].X + 1] != -1))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 是否有砖块水平溢出
		/// </summary>
		/// <returns></returns>
		private bool OverHorlignment()
		{
			for (int i = 0; i < _runningBlock.Cells.Length; i++)
			{
				if ((_runningBlock.ColPos + _runningBlock.Cells[i].X < 0) ||
					(_runningBlock.ColPos + _runningBlock.Cells[i].X >= _colCount) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[i].Y, _runningBlock.ColPos + _runningBlock.Cells[i].X] != -1) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[i].Y, _runningBlock.ColPos + _runningBlock.Cells[i].X] != -1))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 砖块下边溢出
		/// </summary>
		/// <returns></returns>
		private bool OverBottom()
		{
			for (int x = 0; x < _runningBlock.Cells.Length; x++)
			{
				if ((_runningBlock.RowPos + _runningBlock.Cells[x].Y >= _rowCount) ||
					(_cells[_runningBlock.RowPos + _runningBlock.Cells[x].Y, _runningBlock.ColPos + _runningBlock.Cells[x].X] != -1))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// 将当前砖块并入底端砖块
		/// </summary>
		private void CombineBlock()
		{
			for (int i = 0; i < _runningBlock.Cells.Length; i++)
			{
				_cells[_runningBlock.RowPos + _runningBlock.Cells[i].Y, _runningBlock.ColPos + _runningBlock.Cells[i].X] = _runningBlock.ColorIndex;
			}
			CheckFullLine();
		}

		/// <summary>
		/// 擦除当前砖块
		/// </summary>
		private void ClearRunningBlock()
		{
			lock (runGraphics)
			{
				for (int i = 0; i < _runningBlock.Cells.Length; i++)
				{
					runGraphics.FillRectangle(
						_bgbrush,
						(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
						(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
		}

		/// <summary>
		/// 绘制当前砖块
		/// </summary>
		private void DrawRunningBlock()
		{
			lock (runGraphics)
			{
				for (int i = 0; i < _runningBlock.Cells.Length; i++)
				{
					runGraphics.FillRectangle(
						_forebrush[_runningBlock.ColorIndex],
						(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
						(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
		}

		/// <summary>
		/// 重绘预备砖块
		/// </summary>
		private void ReDrawReadyBlock()
		{
			lock (readyGraphics)
			{
				readyGraphics.Clear(_bgColor);
				for (int i = 0; i < _readyBlock.Cells.Length; i++)
				{
					readyGraphics.FillRectangle(
						_forebrush[_readyBlock.ColorIndex],
						(_readyBlock.ColPos + _readyBlock.Cells[i].X) * _rectPic + 1,
						(_readyBlock.RowPos + _readyBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
		}

		/// <summary>
		/// 重绘底部砖块
		/// </summary>
		private void ReDrawCells()
		{
			lock (runGraphics)
			{
				runGraphics.Clear(_bgColor);
				for (int row = 0; row < _rowCount; row++)
				{
					for (int col = 0; col < _colCount; col++)
					{
						if (_cells[row, col] != -1)
							runGraphics.FillRectangle(_forebrush[_cells[row, col]], col * _rectPic + 1, row * _rectPic + 1, _rectPic - 2, _rectPic - 2);
					}
				}
			}
		}

		/// <summary>
		/// 砖块到底或者砖块下面有砖块
		/// 消除可能存在的满行
		/// </summary>
		private void CheckFullLine()
		{
			for (int row = _rowCount - 1; row > 0; row--)
			{
				bool full = true;
				for (int col = 0; col < _colCount; col++)
				{
					if (_cells[row, col] == -1)
					{
						full = false;
						break;
					}
				}
				if (full)
				{
					Score++;
					RemoveLine(row);
					row++;
				}
			}
		}

		/// <summary>
		/// 删除_cells的第Row行
		/// </summary>
		/// <param name="Row">要删除的行的行标</param>
		private void RemoveLine(int Row)
		{
			for (int row = Row; row > 0; row--)
			{
				for (int col = 0; col < _colCount; col++)
				{
					_cells[row, col] = _cells[row - 1, col];
				}
			}
			for (int col = 0; col < _colCount; col++)
			{
				_cells[0, col] = -1;
			}
			ReDrawCells();
		}


		private Timer timer;

		public bool IsWorking
		{
			get
			{
				return timer.Enabled;
			}
		}

		public void Start()
		{
			timer.Start();
		}

		public void Pause()
		{
			timer.Stop();
		}

		public void Continue()
		{
			timer.Start();
		}

		/// <summary>
		/// 顺时针旋转
		/// </summary>
		public void Up()
		{
			lock (runGraphics)
			{
				//擦除旧砖块
				for (int i = 0; i < _runningBlock.Cells.Length; i++)
				{
					runGraphics.FillRectangle(
						_bgbrush,
						(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
						(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
				_runningBlock.Clockwise();
				if (OverHorlignment() || OverBottom())
				{
					_runningBlock.Anticlockwise();
				}
				//绘制新砖块
				for (int i = 0; i < _runningBlock.Cells.Length; i++)
				{
					runGraphics.FillRectangle(
						_forebrush[_runningBlock.ColorIndex],
						(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
						(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
						_rectPic - 2, _rectPic - 2);
				}
			}
		}
		public void Down()
		{
			if (MeetBottom())//砖块到底
			{
				CombineBlock();
				_runningBlock = _readyBlock;
				_runningBlock.ColPos = _colCount / 2;
				_runningBlock.RowPos = 1;
				_readyBlock = Block.GetABlock(_foreColors.Length);
				_readyBlock.ColPos = 2;
				_readyBlock.RowPos = 2;
				DrawRunningBlock();
				ReDrawReadyBlock();
			}
			else//砖块未到底，将砖块下移一格
			{
				ClearRunningBlock();
				_runningBlock.RowPos++;
				DrawRunningBlock();
			}
		}
		public void Left()
		{
			if (MeetLeft())
				return;
			else
			{
				lock (runGraphics)
				{
					//擦除旧砖块
					for (int i = 0; i < _runningBlock.Cells.Length; i++)
					{
						runGraphics.FillRectangle(
							_bgbrush,
							(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
							(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
							_rectPic - 2, _rectPic - 2);
					}
					_runningBlock.ColPos--;
					//绘制新砖块
					for (int i = 0; i < _runningBlock.Cells.Length; i++)
					{
						runGraphics.FillRectangle(
							_forebrush[_runningBlock.ColorIndex],
							(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
							(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
							_rectPic - 2, _rectPic - 2);
					}
				}
			}
		}
		public void Right()
		{
			if (MeetRight())
				return;
			else
			{
				lock (runGraphics)
				{
					//擦除旧砖块
					for (int i = 0; i < _runningBlock.Cells.Length; i++)
					{
						runGraphics.FillRectangle(
							_bgbrush,
							(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
							(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
							_rectPic - 2, _rectPic - 2);
					}
					_runningBlock.ColPos++;
					//绘制新砖块
					for (int i = 0; i < _runningBlock.Cells.Length; i++)
					{
						runGraphics.FillRectangle(
							_forebrush[_runningBlock.ColorIndex],
							(_runningBlock.ColPos + _runningBlock.Cells[i].X) * _rectPic + 1,
							(_runningBlock.RowPos + _runningBlock.Cells[i].Y) * _rectPic + 1,
							_rectPic - 2, _rectPic - 2);
					}
				}
			}
		}

		#region 成员变量
		private int _colCount;
		private int _rowCount;
		private int _rectPic;

		/// <summary>
		/// 当前活动窗格中的所有单元，里面存放着0~_foreColors.Length-1
		/// 如果单元格中存放的数是-1，表明该单元格中应填充背景色
		/// 如果单元格中存放的数index不是-1(而是!~_foreColors.Length-1中的一个数)，
		/// 代表单元格是某砖块的一部分，应该涂以颜色_foreColors[index]
		/// </summary>
		private int[,] _cells;

		private Color _bgColor;
		private Brush _bgbrush;
		private Color[] _foreColors;
		private Brush[] _forebrush;

		private Graphics runGraphics;
		private Graphics readyGraphics;

		private int _score;
		private int Score
		{
			get
			{
				return _score;
			}
			set
			{
				_score = value;
				_labelScore.Text = value.ToString();
			}
		}
		private Label _labelScore;

		/// <summary>
		/// 当前砖块
		/// </summary>
		private Block _runningBlock;
		/// <summary>
		/// 准备好的砖块
		/// </summary>
		private Block _readyBlock;
		#endregion
	}
}
