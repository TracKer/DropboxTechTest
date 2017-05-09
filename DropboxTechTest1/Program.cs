using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DropboxTechTest1 {
  static class Program {
    /// <summary>
    /// Gets the main form in the application.
    /// </summary>
    internal static Form1 MainForm { get; private set; }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args) {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);



      Uri uri = null;
      if (args.Length > 0) {
        // a URI was passed and needs to be handled
        try {
          uri = new Uri(args[0].Trim());
        }
        catch (UriFormatException) {
          Console.WriteLine("Invalid URI.");
        }
      }

      IUriHandler handler = UriHandler.GetHandler();
      if (handler != null) {
        // the singular instance of the application is already running
        if (uri != null) handler.HandleUri(uri);

        // the process will now exit without displaying the main form
        //...
      }
      else {
        // this must become the singular instance of the application
        UriHandler.Register();

        MainForm = new Form1();

        if (uri != null) {
          // a URI was passed, handle it locally
          MainForm.Shown += (o, e) => new UriHandler().HandleUri(uri);
        }

        // load and display the main form
        Application.Run(MainForm);
      }
    }
  }
}
