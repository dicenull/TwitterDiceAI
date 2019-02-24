﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace TwitterDiceAI
{
	class AutoTweeter
	{
		TwitterApp app;
		MarkovSentenceGenerator generator;
        

        /// <param name="interval">投稿間隔</param>
		public AutoTweeter(TwitterApp app, MarkovSentenceGenerator generator, int interval)
		{
			this.app = app;
			this.generator = generator;

			registerFirstTweets();

			Timer timer = new Timer(interval * 60 * 1000);
			timer.Elapsed += Timer_Elapsed;
			timer.Start();

			app.DoTweet(generator.Generate());
		}

        /// <summary>
        /// 文章ジェネレータに過去のツイートを登録する
        /// </summary>
		private void registerFirstTweets()
		{
			foreach (var text in app.GetTweets(100))
			{
				generator.Regist(text);
			}
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			app.DoTweet(generator.Generate());
		}
	}
}
