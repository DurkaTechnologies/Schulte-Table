
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

		private  bool sliderCanExecute = true;
		private  bool restartButtonCanExecute = true;

		private Time currentTime;
		//Time minTime;
		private string timeData;
		private readonly GameType gameMode;

		DispatcherTimer gameTimer;
		private Tile PressedTile;
		private int currentNum;
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
			restartGameCommand = new DelegateCommand(RestartGame, () => restartButtonCanExecute);
			startGameCommand = new DelegateCommand(StartGame , () => true);
			tilePressCommand = new RelayCommand(TilePress, (parameter) => true);
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
			MessageBox.Show("Restart Command Done", "Test");
		}

		public ICommand ToMainMenuCommand => toMainMenuCommand;

		public ICommand RestartGameCommand => restartGameCommand;
		public ICommand TilePressCommand => tilePressCommand;
		
		//public ICommand StartGameCommand => startGameCommand;

		public void TilePress(object parameter)
		{
			PressedTile = (Tile)parameter;
			if (currentNum == 0)
			{
				gameTimer.Start();
				sliderCanExecute = false;
				restartButtonCanExecute = true;

			}
			if (currentNum + 1 == PressedTile.Number)
			{
				CorrectClicks++;
				currentNum++;
				
				PressedTile.IsCorrectPress = PressType.Correct;
				//CheckWin();
			}
			else
			{
				WrongClicks++;
				if (PressedTile.IsCorrectPress == PressType.Correct)
					return; 

				if (PressedTile.IsCorrectPress == PressType.None)
					PressedTile.IsCorrectPress = PressType.Incorrect;
				else if (PressedTile.IsCorrectPress == PressType.Incorrect)
				{
					PressedTile.IsCorrectPress = PressType.None;
					PressedTile.IsCorrectPress = PressType.Incorrect;
				}
			}

		}

		private void SetStartValues()
		{
			currentTime.ResetTime();
			timeData = currentTime.Text;
			BoardSize = 5;
			correctClicks = 0;
			wrongClicks = 0;
			currentNum = 0;
		}

		void timerTick(object sender, EventArgs e)
		{
			currentTime.AddSecond();
			TimerData = currentTime.Text;
		}
	}
}
