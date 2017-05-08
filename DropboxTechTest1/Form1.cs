﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;
using SimpleHttpServer;
using SimpleHttpServer.Models;
using SimpleHttpServer.RouteHandlers;

namespace DropboxTechTest1 {
  public partial class Form1 : Form {
    public HttpServer AppHttpServer;

    public Form1() {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {
//      var Client = new DropboxAppClient(SecretData.DropboxAppKey, SecretData.DropboxAppSecret);
//      DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, "", "http://localhost/auth")

//      this.oauth2State = Guid.NewGuid().ToString("N");

      // Define routes.
      var routeConfig = new List<Route>() {
        new Route {
          Name = "Hello Handler",
          UrlRegex = @"^/$",
          Method = "GET",
          Callable = (HttpRequest request) => {
            return new HttpResponse()
            {
              ContentAsUTF8 = "Hello from SimpleHttpServer",
              ReasonPhrase = "OK",
              StatusCode = "200"
            };
          }
        },
        new Route {
            Name = "FileSystem Static Handler",
            UrlRegex = @"^/Static/(.*)$",
            Method = "GET",
            Callable = new FileSystemRouteHandler() { BasePath = @"C:\Tmp", ShowDirectories=true }.Handle,
        },
      };


      // Run http server.
      AppHttpServer = new HttpServer(48848, routeConfig);
      var thread = new Thread(new ThreadStart(AppHttpServer.Listen));
      thread.Start();


//      var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(
//        OAuthResponseType.Token,
//        SecretData.DropboxAppKey,
//        new Uri("http://localhost:48848/auth")
//      );
//      Process.Start(authorizeUri.ToString());
    }
  }
}