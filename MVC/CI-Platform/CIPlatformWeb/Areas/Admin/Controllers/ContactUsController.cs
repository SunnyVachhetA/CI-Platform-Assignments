using CIPlatform.Entities.ViewModels;
using CIPlatform.Services.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatformWeb.Areas.Admin.Controllers;
[Area("Admin")]
public class ContactUsController : Controller
{
    private readonly IServiceUnit _serviceUnit;
    public ContactUsController(IServiceUnit serviceUnit)
    {
        _serviceUnit = serviceUnit;
    }
    [HttpGet]
    public IActionResult Index()
    {
        IEnumerable<ContactUsVM> messages = _serviceUnit.ContactUsService.LoadAllContactUsMessage();
        return PartialView("_ContactInquires", messages);
    }

    [HttpGet]
    public IActionResult ContactQuery(long contactId)
    {
        try
        {
            ContactUsVM contact = _serviceUnit.ContactUsService.LoadContactMessage(contactId);
            if (contact == null) return NotFound();

            return PartialView("_ContactReply", contact);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while GET contact query : " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpPatch]
    public async Task<IActionResult> ContactResponse(ContactUsVM contact)
    {
        try
        {
            if ( contact == null || string.IsNullOrEmpty(contact.Response)) return NoContent();

            await _serviceUnit.ContactUsService.UpdateContactResponse(contact.Response, contact.ContactId);
            _serviceUnit.ContactUsService.SendContactResponseEmail(contact);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error occured during sending contact response: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }

    [HttpDelete]
    public IActionResult DeleteContact(long contactId)
    {
        try
        {
            _serviceUnit.ContactUsService.DeleteContactEntry(contactId);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while deleting contact entry: " + e.Message);
            Console.WriteLine(e.StackTrace);
            return StatusCode(500);
        }
    }
}
