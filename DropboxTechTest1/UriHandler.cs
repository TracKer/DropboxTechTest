using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;

namespace DropboxTechTest1 {
  public class UriHandler : MarshalByRefObject, IUriHandler {
    const string IPC_CHANNEL_NAME = "SingleInstanceWithUriScheme";

    public static bool Register() {
      try {
        var channel = new IpcServerChannel(IPC_CHANNEL_NAME);
        ChannelServices.RegisterChannel(channel, true);
        RemotingConfiguration.RegisterWellKnownServiceType(
          typeof(UriHandler),
          "UriHandler",
          WellKnownObjectMode.SingleCall
        );

        return true;
      }
      catch {
        Console.WriteLine("Couldn't register IPC channel.");
        Console.WriteLine();
      }

      return false;
    }

    public static IUriHandler GetHandler() {
      try {
        var channel = new IpcClientChannel();
        ChannelServices.RegisterChannel(channel, true);
        var address = String.Format("ipc://{0}/UriHandler", IPC_CHANNEL_NAME);
        var handler = (IUriHandler) RemotingServices.Connect(typeof(IUriHandler), address);

        // need to test whether connection was established
        TextWriter.Null.WriteLine(handler.ToString());

        return handler;
      }
      catch {
        Console.WriteLine("Couldn't get remote UriHandler object.");
        Console.WriteLine();
      }

      return null;
    }

    public bool HandleUri(Uri uri) {
      // this is only a demonstration; a real implementation would:
      // - validate the URI
      // - perform a particular action depending on the URI

      Program.MainForm.AddUri(uri);
      return true;
    }
  }
}
