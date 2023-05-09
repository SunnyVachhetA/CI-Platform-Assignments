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

    public static string GenerateMessageForVolunteerHourApprove(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has approved your volunteer hour timesheet work entry on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your timesheet work hour entry for mission <b>{title}</b> has been approved by admin. Thank you for your contribution.</p>
                    <p>Click on below button to see your updated timesheet details.</p>
                    <a href = '{pageLink}' class='button'> <button>View Timesheet</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForVolunteerHourDecline(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has declined your volunteer hour timesheet work entry on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your timesheet work hour entry for mission <b>{title}</b> has been declined by admin for some reason.</p>
                    <p>If you think this is a mistake please enter work hour entry again or contact admin. Click below to view your updated timesheet.</p>
                    <a href = '{pageLink}' class='button'> <button>View Timesheet</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForVolunteerGoalApprove(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has approved your volunteer goal timesheet work entry on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your timesheet work goal entry for mission <b>{title}</b> has been approved by admin. Thank you for your contribution.</p>
                    <p>Click on below button to see your updated timesheet details.</p>
                    <a href = '{pageLink}' class='button'> <button>View Timesheet</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForVolunteerGoalDecline(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has declined your volunteer hour timesheet goal entry on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your timesheet goal hour entry for mission <b>{title}</b> has been declined by admin for some reason.</p>
                    <p>If you think this is a mistake please enter work goal entry again or contact admin. Click below to view your updated timesheet.</p>
                    <a href = '{pageLink}' class='button'> <button>View Timesheet</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForNewCMSPage(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has added new CMS page on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Check out new CMS page <b>{title}</b> that has been added by admin.</p>
                    <p>Click on below button to see mission details.</p>
                    <a href = '{pageLink}' class='button'> <button>CMS Page</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForMissionApplicationApprove(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has approved your mission application on CI Platform to participate as Volunteer.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>You are now registered as Volunteer for mission <b>{title}</b>. We are happy to have you as volunteer.</p>
                    <p>Click on below button to see mission & timesheet details.</p>
                    <a href = '{pageLink}' class='button'> <button>Mission</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForMissionApplicationDecline(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has declined your mission application on CI Platform to participate as Volunteer.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>You application for registering as volunteer for mission <b>{title}</b> has been declined by admin for some reason. If you think this is a mistake please try contact admin.</p>
                    <p>Click on below button to see mission details or contact admin.</p>
                    <a href = '{pageLink}' class='button'> <button>Mission</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

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
    public static string GenerateStoryInviteMessage(string senderUserName, string storyInviteLink, string toUserName = "", string title = "")
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Story Invitation From Co-Worker</h1>
                </div>
                <div class='body'>
                    <p>Dear {toUserName},</p>
                    <p>You got story invitation for <b>{title}</b> from your co-worker {senderUserName}.</p>
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
    public static string GenerateMissionInviteMessage(string senderUserName, string link, string toUserName = "", string title = "")
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Story Invitation From Co-Worker</h1>
                </div>
                <div class='body'>
                    <p>Dear {toUserName}, </p>
                    <p>You got mission invitation for misison <b>{title}</b> from your co-worker {senderUserName}.</p>
                    <p>Check Out Mission Details By Clicking Below Button</p>
                    
                    <a href = '{link}' class='button'> <button>Mission Information</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }
    public static string GenerateContactResponseMailMessage(string contactSubject, string contactResponse)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Contact Us Response From CI Platform | Admin</h1>
                </div>
                <div class='body'>
                    
                    <h3> You Queried: </h3>
                    <p>{contactSubject}</p>
                    <hr/>
                    <p>Response: {contactResponse}</p>

                    
                    <br>
                    <p>Thank you for contact us! If you're not satisfied with above response please contact us again.</p>
                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }
    public static string GenerateAccountCreationMail(string userName, string email, string password, string link)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Account Creation Notification on CI Platform | Admin</h1>
                </div>
                <div class='body'>
                    
                    <h3>{userName} have been successfully registered on CI Platform.</h3>
                    <p>Your Credentials:</p>
                    <br>
                    <p>Email ID: <b>{email}</b><p>
                    <p>Password: <b>{password}</b></p>
                    <span>(Please change your password after login!)</span>
                    <br>
                    <a href='{link}' class='button'><button>Login Here(Activate Your Account)</button></a>
                    <br>
                    <p>We are looking forward to work with you!</p>
                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }
    public static string GenerateMessageForNewMission(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has added new mission on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Check out new mission <b>{title}</b> that has been added by admin.</p>
                    <p>Click on below button to see mission details.</p>
                    <a href = '{pageLink}' class='button'> <button>Mission</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForStoryApproval(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has approved your story on CI Platform.</h1>
                </div>+
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your story with tite <b>{title}</b> has been approved by admin. Thank you for your generous contribution on platform.</p>
                    <p>Your story will encourage others to take part as well. </p>
                    <p>Click on below button to see your story details on website.</p>
                    <a href = '{pageLink}' class='button'> <button>View Story</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForStoryDecline(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has declined your story on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>You story with title <b>{title}</b> has been declined due to its content or some other reason that might not be appropriate for others.</p>
                    <p>If you think this is a mistake please create your story again or contact admin. Click below to add new story.</p>
                    <a href = '{pageLink}' class='button'> <button>Story Listing</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForCommentApproval(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has approved your comment on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>Your comment on mission tite <b>{title}</b> has been approved by admin. Thank you for your honest comment on mission.</p>
                    <p>Your honest comments and remarks will help us improve.</p>
                    <p>Click on below button to see your comment on website.</p>
                    <a href = '{pageLink}' class='button'> <button>View Comment</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }

    public static string GenerateMessageForCommentDecline(string title, string pageLink, string userName)
    {
        string bodySection = $@"
        <body>
            <div class='container'>
                <div class='header'>
                    <h1>Admin has declined declined on CI Platform.</h1>
                </div>
                <div class='body'>
                    <p>Dear {userName},</p>

                    <p>You comment on mission title <b>{title}</b> has been declined due to its content or some other reason that might not be appropriate for others.</p>
                    <p>If you think this is a mistake please create your comment again or contact admin. Click below to add new comment.</p>
                    <a href = '{pageLink}' class='button'> <button>View Mission</button> </a>

                    <p>Regards,</p>
                    <p>CI Team</p>
                </div>
            </div>
        </body>";

        return CreateEmailBody(bodySection);
    }
}   
