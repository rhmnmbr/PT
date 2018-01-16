using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using PTApi.Data;
using PTApi.Data.Models;

public class Seeder
{
    private readonly PTApiContext _context;
    public Seeder(PTApiContext context)
    {
        _context = context;
    }

    public void MahasiswaSeed()
    {
        if(_context.Mahasiswa.Any()) return;
        var mahasiswaData = System.IO.File.ReadAllText(Path.Combine("Assets","Mahasiswa.json"));
        var mhss = JsonConvert.DeserializeObject<List<Mahasiswa>>(mahasiswaData);
        _context.Mahasiswa.AddRange(mhss);
        _context.SaveChanges();
    }
}