using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_LyThuyet_5_5
{
    public partial class Form1 : Form
    {
        Attachment attachment = null;

        public Form1()
        {
            InitializeComponent();

            tb_Receiver.Text = "hongvinhkrn@gmail.com";
            tb_Sender.Text = "hongvinh45krn@gmail.com";

            tb_Subject.Text = "Test Sending Mail";
            rtb_MailBody.Text = "This is body";
        }

        bool SendMyMail(string sender, string password, string receiver, string mailSubject, string mailBody, Attachment attachment)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(sender);
                    mail.To.Add(receiver);
                    mail.Subject = mailSubject;
                    mail.Body = mailBody;
                    mail.Attachments.Add(attachment);
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(sender, password);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        string SetFormatMail(string bodyMail)
        {
            string myBodyMail = "<h1>" + bodyMail + "</h1>";
            return myBodyMail;
        }

        private void lb_Send_Click(object sender, EventArgs e)
        {
            string senderMail = tb_Sender.Text;
            //string password = tb_Password.Text;
            string password = "vjnh2002";
            string receiver = tb_Receiver.Text;
            string bodyMail = SetFormatMail(rtb_MailBody.Text);
            string subjectMail = tb_Subject.Text;

            if(SendMyMail(senderMail, password, receiver, subjectMail, bodyMail, attachment) == true)
            {
                MessageBox.Show("Đã gửi thành công");
            }
            else
            {
                MessageBox.Show("Lỗi!!! Gửi không thành công");
            }
        }

        bool tb_showPassword = false;
        private void btn_ShowPassword_Click(object sender, EventArgs e)
        {
            if (tb_showPassword)
            {
                tb_Password.UseSystemPasswordChar = false;
                tb_showPassword = false;
            }
            else
            {
                tb_Password.UseSystemPasswordChar = true;
                tb_showPassword = true;
            }
        }

        private void btn_Attach_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.txt;*.docx;*.rtf;*.pdf";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                attachment = new Attachment(openFileDialog.FileName);
            }
        }
    }
}
