using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AuthenticationUtility;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Web;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace D365API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string GetUserSessionOperationPath = string.Format("{0}{1}", URLText.Text, sessionUrlText.Text);
            string responseString;
            string requestString = RequestValue.Text;
            Encoding encoding = Encoding.UTF8;

            Result.Text = string.Empty;
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(requestString);
                var request = HttpWebRequest.Create(GetUserSessionOperationPath);
                request.Headers[OAuthHelper.OAuthHeader] = OAuthHelper.GetAuthenticationHeader(MethodValue.Checked);
                request.Method = "Post";
                //request.ContentLength = 0;
                if (!string.IsNullOrEmpty(requestString))
                {
                    request.ContentLength = byteArray.Length;
                    Stream respStream = request.GetRequestStream();
                    respStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    respStream.Close();
                }
                else
                {
                    request.ContentLength = 0;
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            responseString = streamReader.ReadToEnd();
                        }
                    }
                }
                Result.Text = responseString;
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }
    }
}
