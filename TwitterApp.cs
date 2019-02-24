using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CoreTweet;
using System.Diagnostics;
using CoreTweet.Rest;
using CoreTweet.Core;

namespace TwitterDiceAI
{
	class TwitterApp
	{
		private Tokens tokens;
		private string userName;
		
		public TwitterApp(string userName)
		{
			var appSettings = ConfigurationManager.AppSettings;

			var session = OAuth.Authorize(
				appSettings["ApiKey"],
				appSettings["ApiSecret"]);

			Process.Start(session.AuthorizeUri.AbsoluteUri);
			var pin = Console.ReadLine();

			tokens = OAuth.GetTokens(session, pin);

			this.userName = userName;
		}
		
		public IEnumerable<string> GetTweets(int count)
		{
			return tokens.Statuses.UserTimeline(
				new {
					screen_name = userName,
					count = count,
					exclude_replies = true,
					include_rts = false })
				.Select(tweet => tweet.Text);
		}
		
		public void DoTweet(string text)
		{
			Console.WriteLine($"tweet : {text}");
			tokens.Statuses.Update(new { status = text });
		}
	}
}
