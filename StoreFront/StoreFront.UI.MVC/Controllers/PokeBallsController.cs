using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreFront.DATA.EF.Models;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;

namespace StoreFront.UI.MVC.Controllers
{
    public class PokeBallsController : Controller
    {
        private readonly StoreFrontContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PokeBallsController(StoreFrontContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;//added
        }

        // GET: PokeBalls
        public async Task<IActionResult> Index()
        {
              return View(await _context.PokeBalls.ToListAsync());
        }

        // GET: PokeBalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PokeBalls == null)
            {
                return NotFound();
            }

            var pokeBall = await _context.PokeBalls
                .FirstOrDefaultAsync(m => m.PokeballId == id);
            if (pokeBall == null)
            {
                return NotFound();
            }

            return View(pokeBall);
        }

        // GET: PokeBalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PokeBalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PokeballId,PokeballName,Description,Image,BallImage")] PokeBall pokeBall)
        {
            if (ModelState.IsValid)
            {

                #region File Upload - CREATE
                //Check to see if a file was uploaded
                if (pokeBall.BallImage != null)
                {
                    //Check the file type 
                    //- retrieve the extension of the uploaded file
                    string ext = Path.GetExtension(pokeBall.BallImage.FileName);

                    //- Create a list of valid extensions to check against
                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //- verify the uploaded file has an extension matching one of the extensions in the list above
                    //- AND verify file size will work with our .NET app
                    if (validExts.Contains(ext.ToLower()) && pokeBall.BallImage.Length < 4_194_303)//underscores don't change the number, they just make it easier to read
                    {
                        //Generate a unique filename
                        pokeBall.Image = Guid.NewGuid() + ext;

                        //Save the file to the web server (here, saving to wwwroot/images)
                        //To access wwwroot, add a property to the controller for the _webHostEnvironment (see the top of this class for our example)
                        //Retrieve the path to wwwroot
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        //variable for the full image path --> this is where we will save the image
                        string fullImagePath = webRootPath + "/img/pokeball-img/";

                        //Create a MemoryStream to read the image into the server memory
                        using (var memoryStream = new MemoryStream())
                        {
                            await pokeBall.BallImage.CopyToAsync(memoryStream);//transfer file from the request to server memory
                            using (var img = Image.FromStream(memoryStream))//add a using statement for the Image class (using System.Drawing)
                            {
                                //now, send the image to the ImageUtility for resizing and thumbnail creation
                                //items needed for the ImageUtility.ResizeImage()
                                //1) (int) maximum image size
                                //2) (int) maximum thumbnail image size
                                //3) (string) full path where the file will be saved
                                //4) (Image) an image
                                //5) (string) filename
                                int maxImageSize = 500;//in pixels
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, pokeBall.Image, img, maxImageSize, maxThumbSize);
                                //myFile.Save("path/to/folder", "filename"); - how to save something that's NOT an image

                            }
                        }
                    }
                }
                else
                {
                    //If no image was uploaded, assign a default filename
                    //Will also need to download a default image and name it 'noimage.png' -> copy it to the /images folder
                    pokeBall.Image = "noimage.png";
                }

                #endregion

                _context.Add(pokeBall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokeBall);
        }

        // GET: PokeBalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PokeBalls == null)
            {
                return NotFound();
            }

            var pokeBall = await _context.PokeBalls.FindAsync(id);
            if (pokeBall == null)
            {
                return NotFound();
            }
            return View(pokeBall);
        }

        // POST: PokeBalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PokeballId,PokeballName,Description,Image,BallImage")] PokeBall pokeBall)
        {
            if (id != pokeBall.PokeballId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = pokeBall.Image;

                //Check if the user uploaded a file
                if (pokeBall.BallImage != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(pokeBall.BallImage.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && pokeBall.BallImage.Length < 4_194_303)
                    {
                        //generate a unique file name
                        pokeBall.Image = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/img/pokeball-img/";

                        //Delete the old image
                        if (oldImageName != "noimage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await pokeBall.BallImage.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, pokeBall.Image, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion

                try
                {
                    _context.Update(pokeBall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokeBallExists(pokeBall.PokeballId))
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
            return View(pokeBall);
        }

        // GET: PokeBalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PokeBalls == null)
            {
                return NotFound();
            }

            var pokeBall = await _context.PokeBalls
                .FirstOrDefaultAsync(m => m.PokeballId == id);
            if (pokeBall == null)
            {
                return NotFound();
            }

            return View(pokeBall);
        }

        // POST: PokeBalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PokeBalls == null)
            {
                return Problem("Entity set 'StoreFrontContext.PokeBalls'  is null.");
            }
            var pokeBall = await _context.PokeBalls.FindAsync(id);
            if (pokeBall != null)
            {
                _context.PokeBalls.Remove(pokeBall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokeBallExists(int id)
        {
          return _context.PokeBalls.Any(e => e.PokeballId == id);
        }
    }
}
