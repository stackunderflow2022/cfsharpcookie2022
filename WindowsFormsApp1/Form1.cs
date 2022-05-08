using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public ChromiumWebBrowser Chromebrowser;
        CookieVisitor _cookieVisitor = new CookieVisitor();

        public Form1()
        {
            InitializeComponent();
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            settings.CachePath = Environment.CurrentDirectory + "\\tmp";
            Cef.Initialize(settings);
            Chromebrowser = new ChromiumWebBrowser("https://www.google.com");
            Chromebrowser.TitleChanged += Chromebrowser_TitleChanged;

            panel1.Controls.Add(Chromebrowser);
          

                
        }

        private void Chromebrowser_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            Cef.GetGlobalCookieManager().VisitAllCookies(_cookieVisitor);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitializeChromium();

            button1.Enabled = false; //initialize only once
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }

    class CookieVisitor : ICookieVisitor
    {     
        
        public bool Visit(Cookie cookie, int count, int total, ref bool deleteCookie)
        {
            //here you can store the cookie value to a static class

            string line = cookie.Name + " =  " + cookie.Value + "\n";            

            Console.WriteLine(line);

            return true;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~webview_cookies()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion



    }
}
