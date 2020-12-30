
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
	enum GameType
	{
		GamePlusMode,
		GameMinusMode
	}

	class GamePageViewModel : BaseViewModel
	{
		private GameView gameBoard;

		private bool sliderCanExecute = true;

		private Time currentTime;
		//Time minTime;
		private string timeData;
		private readonly GameType gameMode;

		DispatcherTimer gameTimer;
		private Tile PressedTile;
		private int size;
		private int correctClicks;
		private int wrongClicks;

		private Command toMainMenuCommand;
		private Command restartGameCommand;
		private Command startGameCommand;
		private ICommand tilePressCommand;

		public GamePageViewModel()
		{
			toMainMenuCommand = new DelegateCommand(ToMainMenu, () => true);
			restartGameCommand = new DelegateCommand(RestartGame, RestartButtonCanExecute);
			startGameCommand = new DelegateCommand(StartGame , () => true);
			tilePressCommand = new RelayCommand(TilePress, (parameter) => true);

			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName.Equals(nameof(CorrectClicks)))
					restartGameCommand.RaiseCanExecuteChanged();

				if (args.PropertyName.Equals(nameof(BoardSize)))
					RestartGame();
			};
		}

		private static GamePageViewModel instance;

		public static GamePageViewModel GetInstance()
		{
			if (instance == null)
				instance = new GamePageViewModel();
			return instance;
		}


		public GamePageViewModel(GameType type) 
		{
			gameMode = type;
		}

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

		public int BoardSize
		{
			get => size;
			set
			{
				size = value;
				gameBoard.Size = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(TileList));
				OnPropertyChanged(nameof(QuantityTiles));
			}
		}

		public int QuantityTiles
		{
			get => size*size - 1;
		}

		public void ToMainMenu()
		{
			Navigation.Navigation.Navigate(Navigation.Navigation.MainMenuAlias, new MainMenuModel());
		}
		
		public void StartGame()
		{
			gameBoard = GameView.GetInstance();
			currentTime = new Time();
			gameTimer = new DispatcherTimer();
			gameTimer.Tick += new EventHandler(timerTick);
			gameTimer.Interval = new TimeSpan(0, 0, 1);
			SetStartValues();
		}

		public void RestartGame()
		{
			currentTime.ResetTime();
			gameTimer.Stop();
			TimerData = currentTime.Text;
			CorrectClicks = 0;
			WrongClicks = 0;
			gameBoard.FillGrid();
			OnPropertyChanged(nameof(TileList));

		}

		public ICommand ToMainMenuCommand => toMainMenuCommand;

		public ICommand RestartGameCommand => restartGameCommand;

		public ICommand TilePressCommand => tilePressCommand;
		
		public void TilePress(object parameter)
		{
			PressedTile = (Tile)parameter;
			if (CorrectClicks == 0)
			{
				gameTimer.Start();
				sliderCanExecute = false;

			}
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
			currentTime.ResetTime();
			TimerData = currentTime.Text;
			BoardSize = 5;
			correctClicks = 0;
			wrongClicks = 0;
		}

		void timerTick(object sender, EventArgs e)
		{
			currentTime.AddSecond();
			TimerData = currentTime.Text;
		}

		private void CheckWin()
		{
			if (CorrectClicks == QuantityTiles)
			{
				gameTimer.Stop();

			}
		}

		#region CanExecutes
		private bool RestartButtonCanExecute()
		{
			return CorrectClicks != 0;
		}
		#endregion
	}
}
