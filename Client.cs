﻿using Foundation_GameTemplate;
using REWD.FoundationR;
using System;
using TwitchLib.Client;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Events;
using TwitchLib.Client.Extensions;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;
using ChatFaceZ;

//https://github.com/TwitchLib/TwitchLib
namespace TestConsole
{
	class Program
	{
		static void Start()
		{
			Bot bot = new Bot();
		}
	}

	class Bot
	{
		TwitchClient client;

		public Bot()
		{
			ConnectionCredentials credentials = new ConnectionCredentials(
				Chat_tacular.App.username,
				Chat_tacular.App.auth);
			var clientOptions = new ClientOptions
			{
				MessagesAllowedInPeriod = 750,
				ThrottlingPeriod = TimeSpan.FromSeconds(30)
			};
			WebSocketClient customClient = new WebSocketClient(clientOptions);
			client = new TwitchClient(customClient);
			client.Initialize(credentials, Chat_tacular.App.channel);

			client.OnLog += Client_OnLog;
			client.OnJoinedChannel += Client_OnJoinedChannel;
			client.OnMessageReceived += Client_OnMessageReceived;
			client.OnWhisperReceived += Client_OnWhisperReceived;
			client.OnNewSubscriber += Client_OnNewSubscriber;
			client.OnConnected += Client_OnConnected;

			client.Connect();
		}

		private void Client_OnLog(object sender, OnLogArgs e)
		{
			Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
		}

		private void Client_OnConnected(object sender, OnConnectedArgs e)
		{
			Console.WriteLine($"Connected to {e.AutoJoinChannel}");
		}

		private void Client_OnJoinedChannel(object sender, OnJoinedChannelArgs e)
		{	
			return;
			Console.WriteLine("Hey guys! I am an application connected via TwitchLib!");
			client.SendMessage(e.Channel, "Hey guys! I am an application connected via TwitchLib!");
		}

		private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
		{
			Main.Instance.set_Avatar(e.ChatMessage.Username, e.ChatMessage.Channel, e.ChatMessage.Message, e.ChatMessage.ColorHex == "" ? "#ffffff" : e.ChatMessage.ColorHex);
			if (e.ChatMessage.Message.Contains("badword"))
				client.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
		}

		private void Client_OnWhisperReceived(object sender, OnWhisperReceivedArgs e)
		{
			return;
			if (e.WhisperMessage.Username == "my_friend")
				client.SendWhisper(e.WhisperMessage.Username, "Hey! Whispers are so cool!!");
		}

		private void Client_OnNewSubscriber(object sender, OnNewSubscriberArgs e)
		{
			return;
			if (e.Subscriber.SubscriptionPlan == SubscriptionPlan.Prime)
				client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points! So kind of you to use your Twitch Prime on this channel!");
			else
				client.SendMessage(e.Channel, $"Welcome {e.Subscriber.DisplayName} to the substers! You just earned 500 points!");
		}
	}
}