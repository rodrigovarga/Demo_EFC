﻿using Demo_EFC.Context;
using Demo_EFC.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace Demo_EFC.Controllers
{
    // Controllers/ProdutosController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProdutos()
        {
            var produtos = _context.Produtos
                .Include(p => p.Categoria)
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Preco,
                    Categoria = new
                    {
                        p.Categoria.Id,
                        p.Categoria.Nome
                    }
                })  
                .ToList();
            //var produtos = _context.Produtos.Include(p => p.Categoria).ToList();
            //var produtos = _context.Produtos.ToList();
            return Ok(produtos);
        }

        [HttpPost]
        public IActionResult CreateProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProdutos), new { id = produto.Id }, produto);
        }
    }

}
