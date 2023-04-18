namespace CIPlatform.Services.Utilities;
public static class MailMessageFormatUtility
{
    private const string HeadSection = @"
    <head>
        <style>
            /* Default font size and line height */
            html {
                font-size: 16px;
                line-height: 1.5;
                background-color: '#f7f7f7'
            }

            /* Mobile first styles */
            .container {
                margin: 0 auto;
                max-width: 100%;
                padding: 30px;
                background-color: #ffffff;
                box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
                border-radius: 5px;
            }

            h1 {
                font-size: 24px;
                margin-top: 0;
            }

            button {
                display: inline-block;
                padding: 10px 20px;
                background-color: #f88634;
                color: #ffffff;
                text-decoration: none;
                border-radius: 5px;
                transition: background-color 0.3s ease-in-out;
            }

            button:hover {
                background-color: #ff634;
            }
            /* Add padding and background color to the header */
                    .header {
                        padding: 20px;
                        background-color: #f9f9f9;
                    }

                    /* Add padding and background color to the body */
                    .body {
                        padding: 20px;
                        background-color: #ffffff;
                    }

                    /* Style the heading */
                    h1 {
                        font-size: 24px;
                        margin-top: 0;
                    }
            /* Tablet and desktop styles */
            @media screen and (min-width: 768px) {
                .container {
                    max-width: 600px;
                }
                body
                {
                    font-size: 18px;
                }
            }
        </style>
    </head>";

    public static string GenerateMessageForAccountActivation(string userName, string link)
    {

        string bodySection = $@"
        <body>
            <div class='container'>
                <h1>Welcome {userName},</h1>
                <p>Thank you for registering on CI Platform.</p>
                <p>To activate your account, click on the button below:</p>
                <a href='{link}' style='text-decoration: none;'>
                    <button>Activate Account</button>
                </a>
                <p>Regards,<br>CI Team.</p>
            </div>
        </body>";


        return CreateEmailBody(bodySection);

    }

    public static string GenerateMessageForResetPassword(string link)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Password Reset Request</h1>
                </div>
                <div class='body'>
                    <p>Dear User,</p>
                    <p>We received a request to reset the password for your account. To reset your password, please click on the button below:</p>
                    <a href='{link}' class='button'>
                        <button>Reset Password</button>
                    </a>
                    <p>This link will expire in 30 Minutes, so please make sure to reset your password promptly.</p>
                    <p>If you did not make this request, please ignore this message.</p>
                    <p>Thank you,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }


    public static string GenerateStoryInviteMessage(string userName, string storyInviteLink)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Story Invitation From Co-Worker</h1>
                </div>
                <div class='body'>
                     
                    <p>You got story invite from your co-worker {userName}</p>
                    <p>Check Out Story Details By Clicking Below Button</p>
                    
                    <a href = '{storyInviteLink}' class='button'> <button>Story Information</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    private static string CreateEmailBody(string bodySection)
        =>
            $@"
            <html>
                {HeadSection}
                {bodySection}
            </html>";

    public static string GenerateMissionInviteMessage(string userName, string link)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Story Invitation From Co-Worker</h1>
                </div>
                <div class='body'>
                     
                    <p>You got mission invite from your co-worker {userName}.</p>
                    <p>Check Out Mission Details By Clicking Below Button</p>
                    
                    <a href = '{link}' class='button'> <button>Mission Information</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }
}   
