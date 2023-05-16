// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace MusicCollaborationManager.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>\r\n<html data-editor-version='2' class='sg-campaigns' xmlns='http://www.w3.org/1999/xhtml'>\r\n    <head>\r\n      <meta http-equiv='Content-Type' content='text/html; charset=utf-8'>\r\n      <meta name='viewport' content='width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1'>\r\n      <!--[if !mso]><!-->\r\n      <meta http-equiv='X-UA-Compatible' content='IE=Edge'>\r\n      <!--<![endif]-->\r\n      <!--[if (gte mso 9)|(IE)]>\r\n      <xml>\r\n        <o:OfficeDocumentSettings>\r\n          <o:AllowPNG/>\r\n          <o:PixelsPerInch>96</o:PixelsPerInch>\r\n        </o:OfficeDocumentSettings>\r\n      </xml>\r\n      <![endif]-->\r\n      <!--[if (gte mso 9)|(IE)]>\r\n  <style type='text/css'>\r\n    body {{width: 600px;margin: 0 auto;}}\r\n    table {{border-collapse: collapse;}}\r\n    table, td {{mso-table-lspace: 0pt;mso-table-rspace: 0pt;}}\r\n    img {{-ms-interpolation-mode: bicubic;}}\r\n  </style>\r\n<![endif]-->\r\n      <style type='text/css'>\r\n    body, p, div {{\r\n      font-family: arial,helvetica,sans-serif;\r\n      font-size: 14px;\r\n    }}\r\n    body {{\r\n      color: #000000;\r\n    }}\r\n    body a {{\r\n      color: #1188E6;\r\n      text-decoration: none;\r\n    }}\r\n    p {{ margin: 0; padding: 0; }}\r\n    table.wrapper {{\r\n      width:100% !important;\r\n      table-layout: fixed;\r\n      -webkit-font-smoothing: antialiased;\r\n      -webkit-text-size-adjust: 100%;\r\n      -moz-text-size-adjust: 100%;\r\n      -ms-text-size-adjust: 100%;\r\n    }}\r\n    img.max-width {{\r\n      max-width: 100% !important;\r\n    }}\r\n    .column.of-2 {{\r\n      width: 50%;\r\n    }}\r\n    .column.of-3 {{\r\n      width: 33.333%;\r\n    }}\r\n    .column.of-4 {{\r\n      width: 25%;\r\n    }}\r\n    ul ul ul ul  {{\r\n      list-style-type: disc !important;\r\n    }}\r\n    ol ol {{\r\n      list-style-type: lower-roman !important;\r\n    }}\r\n    ol ol ol {{\r\n      list-style-type: lower-latin !important;\r\n    }}\r\n    ol ol ol ol {{\r\n      list-style-type: decimal !important;\r\n    }}\r\n    @media screen and (max-width:480px) {{\r\n      .preheader .rightColumnContent,\r\n      .footer .rightColumnContent {{\r\n        text-align: left !important;\r\n      }}\r\n      .preheader .rightColumnContent div,\r\n      .preheader .rightColumnContent span,\r\n      .footer .rightColumnContent div,\r\n      .footer .rightColumnContent span {{\r\n        text-align: left !important;\r\n      }}\r\n      .preheader .rightColumnContent,\r\n      .preheader .leftColumnContent {{\r\n        font-size: 80% !important;\r\n        padding: 5px 0;\r\n      }}\r\n      table.wrapper-mobile {{\r\n        width: 100% !important;\r\n        table-layout: fixed;\r\n      }}\r\n      img.max-width {{\r\n        height: auto !important;\r\n        max-width: 100% !important;\r\n      }}\r\n      a.bulletproof-button {{\r\n        display: block !important;\r\n        width: auto !important;\r\n        font-size: 80%;\r\n        padding-left: 0 !important;\r\n        padding-right: 0 !important;\r\n      }}\r\n      .columns {{\r\n        width: 100% !important;\r\n      }}\r\n      .column {{\r\n        display: block !important;\r\n        width: 100% !important;\r\n        padding-left: 0 !important;\r\n        padding-right: 0 !important;\r\n        margin-left: 0 !important;\r\n        margin-right: 0 !important;\r\n      }}\r\n      .social-icon-column {{\r\n        display: inline-block !important;\r\n      }}\r\n    }}\r\n  </style>\r\n    <style>\r\n      @media screen and (max-width:480px) {{\r\n        table\\0 {{\r\n          width: 480px !important;\r\n          }}\r\n      }}\r\n    </style>\r\n      <!--user entered Head Start--><!--End Head user entered-->\r\n    </head>\r\n    <body>\r\n      <center class='wrapper' data-link-color='#1188E6' data-body-style='font-size:14px; font-family:arial,helvetica,sans-serif; color:#000000; background-color:#FFFFFF;'>\r\n        <div class='webkit'>\r\n          <table cellpadding='0' cellspacing='0' border='0' width='100%' class='wrapper' bgcolor='#FFFFFF'>\r\n            <tr>\r\n              <td valign='top' bgcolor='#FFFFFF' width='100%'>\r\n                <table width='100%' role='content-container' class='outer' align='center' cellpadding='0' cellspacing='0' border='0'>\r\n                  <tr>\r\n                    <td width='100%'>\r\n                      <table width='100%' cellpadding='0' cellspacing='0' border='0'>\r\n                        <tr>\r\n                          <td>\r\n                            <!--[if mso]>\r\n    <center>\r\n    <table><tr><td width='600'>\r\n  <![endif]-->\r\n                                    <table width='100%' cellpadding='0' cellspacing='0' border='0' style='width:100%; max-width:600px;' align='center'>\r\n                                      <tr>\r\n                                        <td role='modules-container' style='padding:0px 0px 0px 0px; color:#000000; text-align:left;' bgcolor='#FFFFFF' width='100%' align='left'><table class='module preheader preheader-hide' role='module' data-type='preheader' border='0' cellpadding='0' cellspacing='0' width='100%' style='display: none !important; mso-hide: all; visibility: hidden; opacity: 0; color: transparent; height: 0; width: 0;'>\r\n    <tr>\r\n      <td role='module-content'>\r\n        <p></p>\r\n      </td>\r\n    </tr>\r\n  </table><table class='wrapper' role='module' data-type='image' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='e182a503-36a1-4f11-9733-0e9a772061f4'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='font-size:6px; line-height:10px; padding:0px 0px 0px 0px;' valign='top' align='center'>\r\n          <img class='max-width' border='0' style='display:block; color:#000000; text-decoration:none; font-family:Helvetica, arial, sans-serif; font-size:16px; max-width:32% !important; width:32%; height:auto !important;' width='192' alt='' data-proportionally-constrained='true' data-responsive='true' src='http://cdn.mcauto-images-production.sendgrid.net/9b575e932158a85e/7cc18668-7a45-4d90-9f3c-1660a9b628a1/2365x2127.png'>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='2a0c990b-a8d2-4692-b88b-992e43d7268e'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div><div style='font-family: inherit; text-align: center'><span style='font-size: 36px'>Music Collaboration Manager</span></div><div></div></div></td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class='module' role='module' data-type='divider' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='0362c533-d08f-4a69-b5f8-5d18c20a7737'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='padding:0px 0px 0px 0px;' role='module-content' height='100%' valign='top' bgcolor=''>\r\n          <table border='0' cellpadding='0' cellspacing='0' align='center' width='100%' height='2px' style='line-height:2px; font-size:2px;'>\r\n            <tbody>\r\n              <tr>\r\n                <td style='padding:0px 0px 2px 0px;' bgcolor='#000000'></td>\r\n              </tr>\r\n            </tbody>\r\n          </table>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='40b1cd7a-1393-47d4-8443-aa45d7c1a335'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='padding:0px 0px 20px 0px;' role='module-content' bgcolor=''>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class='module' role='module' data-type='text' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='d117991d-837c-454b-b39f-00dfbba0a993'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='padding:18px 0px 18px 0px; line-height:22px; text-align:inherit;' height='100%' valign='top' bgcolor='' role='module-content'><div><div style='font-family: inherit; text-align: center'>Click below to reset your password.</div><div></div></div></td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table class='module' role='module' data-type='spacer' border='0' cellpadding='0' cellspacing='0' width='100%' style='table-layout: fixed;' data-muid='954ede28-6bb2-4ca6-8864-ae6290ab12ee'>\r\n    <tbody>\r\n      <tr>\r\n        <td style='padding:0px 0px 16px 0px;' role='module-content' bgcolor=''>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table><table border='0' cellpadding='0' cellspacing='0' class='module' data-role='module-button' data-type='button' role='module' style='table-layout:fixed;' width='100%' data-muid='13a0a89f-45ef-44ab-a0e8-9d09ef84aec0'>\r\n      <tbody>\r\n        <tr>\r\n          <td align='center' bgcolor='' class='outer-td' style='padding:0px 0px 0px 0px;'>\r\n            <table border='0' cellpadding='0' cellspacing='0' class='wrapper-mobile' style='text-align:center;'>\r\n              <tbody>\r\n                <tr>\r\n                <td align='center' bgcolor='#333333' class='inner-td' style='border-radius:6px; font-size:16px; text-align:center; background-color:inherit;'>\r\n                  <a href='{HtmlEncoder.Default.Encode(callbackUrl)}' style='background-color:#333333; border:1px solid #333333; border-color:#333333; border-radius:6px; border-width:1px; color:#ffffff; display:inline-block; font-size:21px; font-weight:normal; letter-spacing:0px; line-height:normal; padding:12px 18px 12px 18px; text-align:center; text-decoration:none; border-style:solid;' target='_blank'>Change Password</a>\r\n                </td>\r\n                </tr>\r\n              </tbody>\r\n            </table>\r\n          </td>\r\n        </tr>\r\n      </tbody>\r\n    </table><div data-role='module-unsubscribe' class='module' role='module' data-type='unsubscribe' style='color:#444444; font-size:12px; line-height:20px; padding:16px 16px 16px 16px; text-align:Center;' data-muid='4e838cf3-9892-4a6d-94d6-170e474d21e5'><div class='Unsubscribe--addressLine'><p class='Unsubscribe--senderName' style='font-size:12px; line-height:20px;'>PandaProgrammers</p><p style='font-size:12px; line-height:20px;'><span class='Unsubscribe--senderAddress'>347 Monmouth Ave N</span>, <span class='Unsubscribe--senderCity'>Monmouth</span>, <span class='Unsubscribe--senderState'>OR</span> <span class='Unsubscribe--senderZip'>97068</span></p></div><p style='font-size:12px; line-height:20px;'><a class='Unsubscribe--unsubscribeLink' href='' target='_blank' style=''>Unsubscribe</a> - <a href='' target='_blank' class='Unsubscribe--unsubscribePreferences' style=''>Unsubscribe Preferences</a></p></div></td>\r\n                                      </tr>\r\n                                    </table>\r\n                                    <!--[if mso]>\r\n                                  </td>\r\n                                </tr>\r\n                              </table>\r\n                            </center>\r\n                            <![endif]-->\r\n                          </td>\r\n                        </tr>\r\n                      </table>\r\n                    </td>\r\n                  </tr>\r\n                </table>\r\n              </td>\r\n            </tr>\r\n          </table>\r\n        </div>\r\n      </center>\r\n    </body>\r\n  </html>");
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
