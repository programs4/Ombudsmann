<%@ Page Language="C#" %>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        // Create a random code and store it in the Session object.
        Session["Captcha"] = new Random().Next(1111, 9999);

        // Create a CAPTCHA image using the text stored in the Session object.
        RandomImage Ri = new RandomImage(Session["Captcha"]._ToString(), 300, 75);

        // Change the response headers to output a JPEG image.
        this.Response.Clear();
        this.Response.ContentType = "image/jpeg";

        // Write the image to the response stream in JPEG format.
        Ri.Image.Save(this.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

        // Dispose of the CAPTCHA image object.
        Ri.Dispose();
    }
</script>
