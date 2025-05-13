﻿using Demo_EFC.Context;
using Demo_EFC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_EFC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetCategorias()
        {
            var categorias = _context.Categorias
                .Include(c => c.Produtos)
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                    Produtos = c.Produtos.Select(p => new
                    {
                        p.Id,
                        p.Nome,
                        p.Preco
                    })
                })
                .ToList();
            //var categorias = _context.Categorias.Include(c => c.Produtos).ToList();
            //var categorias = _context.Categorias.ToList();
            return Ok(categorias);
        }

        [HttpPost]
        public IActionResult CreateCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCategorias), new { id = categoria.Id }, categoria);
        }
    }
}
