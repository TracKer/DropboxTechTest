using System;
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

namespace DropboxTechTest1 {
  public partial class Form1 : Form {
    public Form1() {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e) {
//      var Client = new DropboxAppClient(SecretData.DropboxAppKey, SecretData.DropboxAppSecret);
//      DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Token, "", "http://localhost/auth")

//      this.oauth2State = Guid.NewGuid().ToString("N");

      var authorizeUri = DropboxOAuth2Helper.GetAuthorizeUri(
        OAuthResponseType.Token,
        SecretData.DropboxAppKey,
        new Uri("http://localhost:48848/auth")
      );
      Process.Start(authorizeUri.ToString());
    }
  }
}
