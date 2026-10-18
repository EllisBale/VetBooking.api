namespace VetBooking.api.Data;

using Microsoft.EntityFrameworkCore;
using VetBooking.api.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{

   

}

 