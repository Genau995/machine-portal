using MachinePortal.Data;
using MachinePortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class MachineController : Controller
{
    private readonly AppDbContext _context;

    public MachineController(AppDbContext context)
    {
        _context = context;
    }
    // ================= EDIT =================

    // GET: Edit
    public IActionResult Edit(int id)
    {
        var item = _context.MachineEntries.Find(id);
        if (item == null) return NotFound();

        return View(item);
    }

    // POST: Edit
    [HttpPost]
    public IActionResult Edit(MachineEntry entry)
    {
        if (ModelState.IsValid)
        {
            _context.MachineEntries.Update(entry);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(entry);
    }


    // ================= DELETE =================

    // GET: Delete
    public IActionResult Delete(int id)
    {
        var item = _context.MachineEntries.Find(id);
        if (item == null) return NotFound();

        return View(item);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var item = _context.MachineEntries.Find(id);

        if (item != null)
        {
            _context.MachineEntries.Remove(item);
            _context.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    // GET: Create page
    public IActionResult Create()
    {
        return View();
    }

    // POST: Save data
   
    [HttpPost]
    public async Task<IActionResult> Create(MachineEntry entry)
    {
        if (ModelState.IsValid)
        {
            // Save to database
            _context.MachineEntries.Add(entry);
            _context.SaveChanges();

            // Send to Google Sheet
            using (var client = new HttpClient())
            {
                var url = "https://script.google.com/macros/s/AKfycbzESjM0kzBBBCqKsW2ysKAH9JbNneF3nX_5HhUak_1Y2MrLKtKoxvvHBTzFkKfWp9tu8g/exec";

                var data = new
                {
                    TechnicianName = entry.TechnicianName,
                    BankName = entry.BankName,
                    Branch = entry.Branch,
                    MachineType = entry.MachineType,
                    SerialNumber = entry.SerialNumber,
                    SoftwareVersion = entry.SoftwareVersion
                };

                var json = System.Text.Json.JsonSerializer.Serialize(data);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                await client.PostAsync(url, content);
            }
            TempData["Success"] = "Record Saved Successfully!";
            return RedirectToAction("Create");
        }

        return View(entry);
    }

    // GET: Show all data
    public IActionResult Index()
    {
        var data = _context.MachineEntries.ToList();
        return View(data);
    }
}