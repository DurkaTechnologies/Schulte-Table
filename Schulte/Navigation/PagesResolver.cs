using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Schulte;

namespace Schulte.Navigation
{
    public class PagesResolver 
    {
        
        private readonly Dictionary<string, Func<Page>> pagesResolvers = new Dictionary<string, Func<Page>>();

        public PagesResolver()
        {
			pagesResolvers.Add(Navigation.MainMenuAlias, () => new MainMenu());
			pagesResolvers.Add(Navigation.GameBoardAlias, () => new GamePage());
			pagesResolvers.Add(Navigation.StatisticAlias, () => new StatisticPage());
			pagesResolvers.Add(Navigation.AboutAlias, () => new AboutPage());
		}

        public Page GetPageInstance(string alias)
        {
			if (pagesResolvers.ContainsKey(alias))
				return pagesResolvers[alias]();

			return pagesResolvers[Navigation.MainMenuAlias]();
		}
	}
}
