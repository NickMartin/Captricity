using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Captricity
{
    public partial class _Default : System.Web.UI.Page
    {
        static string _fileUploadDestination = "ImageUploads";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if(Request.Files.Count > 0)
            {
                HttpFileCollection uploadedFiles = Request.Files;

                
                try
                {
                    int count = 0;
                    List<string> rejectedFiles = new List<string>();

                    for(int i = 0; i < uploadedFiles.Count; i++)
                    {
                        HttpPostedFile userPostedFile = uploadedFiles[i];

                        if(userPostedFile.ContentType == "image/jpeg"
                            || userPostedFile.ContentType == "image/png"
                            || userPostedFile.ContentType == "application/pdf")
                        {
                            if(userPostedFile.ContentLength < (1024 * 4000))
                            {
                                string filename = Path.GetFileName(userPostedFile.FileName);
                                userPostedFile.SaveAs(Server.MapPath("~/" + _fileUploadDestination + "/") + filename);
                                StatusLabel.Text = "Upload status: " + filename + " uploaded!";
                                count++;
                            }
                            else
                            {
                                StatusLabel.Text = "Upload status: The file has to be less than 4000 kb!";
                                rejectedFiles.Add(userPostedFile.FileName);
                            }
                        }
                        else
                        {
                            StatusLabel.Text = "Upload status: Only JPEG, PNG, or PDF files are accepted!";
                            rejectedFiles.Add(userPostedFile.FileName);
                        }
                    }

                    StatusLabel.Text = "Upload status: " + count + " files uploaded!";
                    StatusLabel.Text += "Files rejected: ";

                    foreach(string reject in rejectedFiles)
                    {
                        StatusLabel.Text += " " + reject;
                    }

                }
                catch(Exception ex)
                {
                    StatusLabel.Text = "Upload status: The files could not be uploaded. The following error occured: " + ex.Message;
                }

                
            }
        }
    }
}
