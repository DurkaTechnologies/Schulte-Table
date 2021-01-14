using Schulte.Commands;
using Schulte.UserControls;
using Schulte.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace Schulte.ViewModels
{
	#region Enums
	enum GameType
	{
		GamePlusMode,
		GameMinusMode
	}

	enum GameСomplexity
	{
		Easy,
		Middle,
		Hard
	}

	enum GameStatus
	{
		InProgress,
		GameWin,
		GameLost
	}
	#endregion

	class GamePageViewModel : BaseViewModel
	{
		delegate bool СomplexityCanExecute();
		private GameView gameBoard;
		private int size;

		private Tile PressedTile;
		private bool tilePressExecute = true;
		private bool schulteMode = false;
		private int correctClicks;
		private int wrongClicks;

		private GameType gameType;
		private GameStatus gameStatus;
		private GameСomplexity gameСomplexity;

		private Time currentTime;
		DispatcherTimer gameTimer;
		private string timeData;

		#region Commands
		private Command toMainMenuCommand;
		private Command restartGameCommand;
		private Command setEasyModeCommand;
		private Command setMiddleModeCommand;
		private Command setHardModeCommand;
		private Command setSchulteModeCommand;
		private Command startGameCommand;
		#endregion

		public GamePageViewModel()
		{
			InitializeCommands();
			InitializePropertyChanged();
		}

		public GamePageViewModel(GameType type) : this()
		{
			GameType = type;
		}

		private void InitializeCommands()
		{
			toMainMenuCommand = new DelegateCommand(ToMainMenu, () => true);
			restartGameCommand = new DelegateCommand(RestartGame, RestartButtonCanExecute);
			startGameCommand = new DelegateCommand(StartGame, () => true);
			TilePressCommand = new RelayCommand(TilePress, (parameter) => tilePressExecute);

			setEasyModeCommand = new DelegateCommand(SetEasyСomplexity, SetEasyСomplexityButtonCanExecute);
			setMiddleModeCommand = new DelegateCommand(SetMiddleСomplexity, SetMiddleСomplexityButtonCanExecute);
			setHardModeCommand = new DelegateCommand(SetHardСomplexity, SetHardСomplexityButtonCanExecute);
			setSchulteModeCommand = new DelegateCommand(SetSchulteMode, () => CorrectClicks == 0 || CorrectClicks == 0);
		}

		private void InitializePropertyChanged()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(CorrectClicks)))
					restartGameCommand.RaiseCanExecuteChanged();

				if (args.PropertyName.Equals(nameof(BoardSize)))
					RestartGame();

				if (args.PropertyName.Equals(nameof(CorrectClicks)) || args.PropertyName.Equals(nameof(WrongClicks)))
				{
					RaiseCanExecuteChanged();
					setSchulteModeCommand.RaiseCanExecuteChanged();
				}
				if (args.PropertyName.Equals(nameof(SchulteMode)))
				{
					if (SchulteMode)
						gameBoard.SetSchulteMode();
					else
						gameBoard.SetDefaultMode();
				}

			};
		}

		#region Singelton
		private static GamePageViewModel instance;

		public static GamePageViewModel GetInstance()
		{
			if (instance == null)
				instance = new GamePageViewModel();
			return instance;
		}
		public static GamePageViewModel GetInstance(GameType type)
		{
			if (instance == null)
				instance = new GamePageViewModel(type);
			else
				instance.GameType = type;
			return instance;
		}
		#endregion

		#region SETGET

		public ICollection<RoundButton> TileList
		{
			get => gameBoard.TileCollection;
			set
			{
				gameBoard.TileCollection = value;
				OnPropertyChanged();
			}
		}
		
		public int CorrectClicks
		{
			get => correctClicks;
			set
			{
				correctClicks = value;
				OnPropertyChanged();
			}
		}

		public GameStatus GameStatus
		{
			get => gameStatus;
			set
			{
				gameStatus = value;
				OnPropertyChanged();
			}
		}

		public int WrongClicks
		{
			get => wrongClicks;
			set
			{
				wrongClicks = value;
				OnPropertyChanged();
			}
		}

		public string TimerData
		{
			get => timeData;
			set
			{
				timeData = value;
				OnPropertyChanged();
			}
		}

		public bool SchulteMode
		{
			get => schulteMode;
			set
			{
				schulteMode = value;
				OnPropertyChanged();
			}
		}

		public int BoardSize
		{
			get => size;
			set
			{
				if (size != value)
				{
					
					size = value;
					gameBoard.Size = value;
					if (size == default)
						OnPropertyChanged();
					OnPropertyChanged(nameof(TileList));
					OnPropertyChanged(nameof(QuantityTiles));
				}
			}
		}

		public int QuantityTiles => size * size - 1;

		public GameType GameType
		{
			get => gameType;
			set => gameType = value;
		}
		#endregion

		#region ICommandGetters
		public ICommand ToMainMenuCommand => toMainMenuCommand;
		public ICommand RestartGameCommand => restartGameCommand;
		public ICommand TilePressCommand { get; private set; }
		public ICommand SetEasyModeCommand => setEasyModeCommand;
		public ICommand SetMiddleModeCommand => setMiddleModeCommand;
		public ICommand SetHardModeCommand => setHardModeCommand;
		public ICommand SetSchulteModeCommand => setSchulteModeCommand;
		#endregion

		public void ToMainMenu()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, new MainMenuModel());
		}
		
		public void StartGame()
		{
			gameBoard = GameView.GetInstance();
			currentTime = new Time();
			gameTimer = new DispatcherTimer();
			gameTimer.Tick += new EventHandler(TimerTick);
			gameTimer.Interval = new TimeSpan(0, 0, 1);
			gameTimer.Stop();
			SetStartValues();
		}

		public void RestartGame()
		{
			if (GameType == GameType.GamePlusMode)
				currentTime.ResetTime();
			else
			{
				switch (gameСomplexity)
				{
					case GameСomplexity.Easy:
						SetEasyСomplexity();
						break;
					case GameСomplexity.Middle:
						SetMiddleСomplexity();
						break;
					case GameСomplexity.Hard:
						SetHardСomplexity();
						break;
				}
			}
			GameStatus = GameStatus.InProgress;
			tilePressExecute = true;

			gameTimer.Stop();
			TimerData = currentTime.Text;
			CorrectClicks = 0;
			WrongClicks = 0;
			gameBoard.FillGrid();
			OnPropertyChanged(nameof(TileList));
			OnPropertyChanged(nameof(SchulteMode));

		}

		public void TilePress(object parameter)
		{
			PressedTile = (Tile)parameter;
			if (CorrectClicks == 0)
				gameTimer.Start();

			if (CorrectClicks + 1 == PressedTile.Number)
			{
				CorrectClicks++;
				PressedTile.IsCorrectPress = PressType.Correct;
				CheckWin();
			}
			else
			{
				WrongClicks++;
				if (PressedTile.IsCorrectPress == PressType.Correct)
					return;
				PressedTile.IsCorrectPress = PressType.None;
				PressedTile.IsCorrectPress = PressType.Incorrect;
			}
		}

		private void SetStartValues()
		{
			if (GameType == GameType.GamePlusMode)
				currentTime.ResetTime();
			else
			{
				switch (gameСomplexity)
				{
					case GameСomplexity.Easy:
						SetEasyСomplexity();
						break;
					case GameСomplexity.Middle:
						SetMiddleСomplexity();
						break;
					case GameСomplexity.Hard:
						SetHardСomplexity();
						break;
				}
			}

			TimerData = currentTime.Text;
			BoardSize = 5;
			correctClicks = 0;
			wrongClicks = 0;
			GameStatus = GameStatus.InProgress;
		}

		private void TimerTick(object sender, EventArgs e)
		{
			if (GameType == GameType.GameMinusMode)
				currentTime.MinusSecond();
			else
				currentTime.AddSecond();

			TimerData = currentTime.Text;

			if (currentTime.TimeIsReset && CorrectClicks < QuantityTiles)
				GameOver();
		}

		private void CheckWin()
		{
			if (CorrectClicks == QuantityTiles)
			{
				Win();
			}
		}

		private void Win()
		{
			gameTimer.Stop();
			OnPropertyChanged(nameof(TileList));
			GameStatus = GameStatus.GameWin;
		}

		private void GameOver()
		{
			gameTimer.Stop();
			tilePressExecute = false;
			GameStatus = GameStatus.GameLost;
		}

		private void SetEasyСomplexity()
		{
			currentTime.SetTime(StartTimeResolver.EasyModeTime);
			TimerData = currentTime.Text;
			gameСomplexity = GameСomplexity.Easy;
			RaiseCanExecuteChanged();

		}
		private void SetMiddleСomplexity()
		{
			currentTime.SetTime(StartTimeResolver.MiddleModeTime);
			TimerData = currentTime.Text;
			gameСomplexity = GameСomplexity.Middle;
			RaiseCanExecuteChanged();

		}
		private void SetHardСomplexity()
		{
			currentTime.SetTime(StartTimeResolver.HardModeTime);
			TimerData = currentTime.Text;
			gameСomplexity = GameСomplexity.Hard;
			RaiseCanExecuteChanged();

		}

		private void SetSchulteMode()
		{
			SchulteMode = !SchulteMode;


		}


		#region CanExecutes
		private bool RestartButtonCanExecute()
		{
			return CorrectClicks != 0;
		}

		private bool SetСomplexityButtonCanExecute(GameСomplexity сomplexity)
		{
			if (CorrectClicks != 0 || CorrectClicks != 0)
				return false;
			else if (gameСomplexity == сomplexity)
				return false;
			return true;
		}

		private bool SetEasyСomplexityButtonCanExecute()
		{ 
			return SetСomplexityButtonCanExecute(GameСomplexity.Easy);
		}
		private bool SetMiddleСomplexityButtonCanExecute()
		{
			return SetСomplexityButtonCanExecute(GameСomplexity.Middle);
		}
		private bool SetHardСomplexityButtonCanExecute()
		{
			return SetСomplexityButtonCanExecute(GameСomplexity.Hard); 
		}

		private void RaiseCanExecuteChanged()
		{
			setEasyModeCommand.RaiseCanExecuteChanged();
			setMiddleModeCommand.RaiseCanExecuteChanged();
			setHardModeCommand.RaiseCanExecuteChanged();
		}
		#endregion
	}
}
