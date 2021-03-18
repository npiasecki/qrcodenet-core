# qrcodenet-core

This is a fork of the old https://archive.codeplex.com/?p=qrcodenet with everything but SVG rendering removed and rebuilt under .NET Core 3.1

My "itch" is I just needed a QR code dumped out on a page as an SVG. I used in combination with [PureOtp](https://github.com/coinigy/PureOtp) and [Base32](https://www.nuget.org/packages/Base32) library. 

Here's how I use it in my ASP.NET Core app:

1. Create a shared secret

        this.SharedSecret = Base32Encoder.Encode(KeyGeneration.GenerateRandomKey(10));

2. Mangle it into a URL

        this.QRCodeUrl = $"otpauth://totp/Contoso?secret={this.SharedSecret}";

3. Spit out a tag helper on your Razor page

        <svt-qr-code content="@Model.QRCodeUrl"></svt-qr-code>
        
4. Write the tag helper

        [HtmlTargetElement("svt-qr-code")]
        public class QRCodeTagHelper : TagHelper
        {
            public string? Content
            {
                get;
                set;
            }

            public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
            {
                await base.ProcessAsync(context, output);

                var encoder = new QrEncoder(ErrorCorrectionLevel.H);
                var encoded = encoder.Encode(this.Content);
                var renderer = new SVGRenderer(
                    new FixedCodeSize(200, QuietZoneModules.Two), 
                    new FormColor(Color.Black), 
                    new FormColor(Color.White));
                var svg = renderer.WriteToString(encoded.Matrix, true);
                output.Content.SetHtmlContent(svg);
            }
        }

  Perhaps I am a fool. It works for me.
