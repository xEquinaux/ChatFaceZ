using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace Chat_tacular;

public partial class Login : Window, IComponentConnector
{
	private bool closing = true;

	public Login()
	{
		InitializeComponent();
	}

	private void button_verify_Click(object sender, RoutedEventArgs e)
	{
		Process process = LoadURL();
		while (!process.HasExited)
		{
			Task.WaitAll(Task.Delay(1000));
		}
	}

	private void button_exitClick(object sender, RoutedEventArgs e)
	{
		closing = false;
		App.auth = "oauth:" + box_oauth.Password;
		App.username = box_username.Text;
		App.channel = box_channel.Text;
		if (App.auth.Length > 0 && App.username.Length > 3 && App.channel.Length > 3)
		{
			Close();
		}
	}
	
	private HttpWebResponse GetUrl(IDictionary<string, string> values, string URL)
	{
		HttpWebRequest obj = (HttpWebRequest)WebRequest.Create(App.EncodeURL(values, URL, ""));
		obj.Method = "GET";
		obj.KeepAlive = true;
		obj.AllowAutoRedirect = true;
		obj.Accept = "*/*";
		return (HttpWebResponse)obj.GetResponse();
	}

	private Process LoadURL(string URL = "https://id.twitch.tv/oauth2/authorize")
	{
		return Process.Start(GetUrl(new Dictionary<string, string>
		{
			{ "client_id", "l2hfsdik4txfm297h4yguxqhad9x21" },
			{ "redirect_uri", "https://dev.circleprefect.com/auth.php" },
			{ "response_type", "token" },
			{ "scope", "chat:edit chat:read" }
		}, URL).ResponseUri.AbsoluteUri);
	}
}
