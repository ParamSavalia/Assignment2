using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab4.Data;
using Lab4.Models;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Azure;
using System.IO;

/*  
 *  Student Name : Param Savalia (040963842), Kunjesh Aghera (040980391)
 *  Reference: : https://github.com/aarad-ac/EFCore
 *               https://github.com/ParamSavalia/Lab4 
 *  Version : 1.0
*/



namespace Lab4.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "ads";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        // GET: Advertisements
        public async Task<IActionResult> Index()
        {
            return View(await _context.Communities.ToListAsync());
        }

        public async Task<IActionResult> AdsForCommunity(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            ViewBag.Community = community;
            List<Advertisement> myAds = _context.Advertisement.Where(x => x.Community.Id == community.Id).ToList();
            return View("Index", myAds);
        }



        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        public IActionResult Create(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = _context.Communities.Find(id);
            if (community == null)
            {
                return NotFound();
            }

            ViewBag.Community = community;
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FileName,Url")] Advertisement advertisement)
        public async Task<IActionResult> Create(String fileName, IFormFile file, String communityId)

        {
            if (communityId == null)
            {
                return NotFound();
            }

            var community = _context.Communities.Find(communityId);
            if (community == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {

                BlobContainerClient containerClient;
                try
                {
                    containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                    // Give access to public
                    containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
                }
                catch (RequestFailedException)
                {
                    containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                }

                try
                {
                    string randomFileName = fileName;
                    // create the blob to hold the data
                    var blockBlob = containerClient.GetBlobClient(randomFileName);
                    if (await blockBlob.ExistsAsync())
                    {
                        await blockBlob.DeleteAsync();
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        // copy the file data into memory
                        await file.CopyToAsync(memoryStream);

                        // navigate back to the beginning of the memory stream
                        memoryStream.Position = 0;

                        // send the file to the cloud
                        await blockBlob.UploadAsync(memoryStream);
                        memoryStream.Close();
                    }

                    // add the photo to the database if it uploaded successfully
                    var image = new Advertisement
                    {
                        Url = blockBlob.Uri.AbsoluteUri,
                        FileName = randomFileName,
                        Community = community
                    };

                    _context.Advertisement.Add(image);
                    _context.SaveChanges();
                }
                catch (RequestFailedException)
                {
                    View("Error");
                }
                ViewBag.Community = community;
                return RedirectToAction("AdsForCommunity", "Advertisements",new { id = community.Id });
            }
            ViewBag.Community = community;
            return RedirectToAction("AdsForCommunity", "Advertisements", new { id = community.Id });
        }

        // GET: Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FileName,Url")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var advertisement = await _context.Advertisement.FindAsync(id);
            _context.Advertisement.Remove(advertisement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
            return _context.Advertisement.Any(e => e.Id == id);
        }
    }
}
