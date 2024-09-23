using back_end.Data;
using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.Repositories.ProductRepositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    // Criar produto no banco
    public async Task<Product> CreateProductAsync(Product product)
    {
        // Gera o ID tipo GUID
        product.Id = Guid.NewGuid();
        
        // Adiciona e salva o produto no banco
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        
        // Retorna o produto
        return product;
    }
    
    // Atualizar produto no banco
    public async Task<Product?> UpdateProductAsync(Product product, Guid id)
    {
        try
        {
            var toUpdateProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (toUpdateProduct == null)
            {
                return null; // Retorno null se não encontrar o produto com o id
            }

            // Atualiza as propriedades
            toUpdateProduct.Name = product.Name;
            toUpdateProduct.Price = product.Price;
            toUpdateProduct.Quantity = product.Quantity;

            await _context.SaveChangesAsync();
            return toUpdateProduct; // Retorna o produto atualizado
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Produto não foi encontrado.", e);
        }
    }
    
    // Deleta produto por ID
    public async Task<Product?> DeleteProductByIdAsync(Guid id)
    {
        try
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            // Remove o produto do banco, salva e retorna o próprio produto removido
            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();
            return existingProduct;
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Não foi possível deletar produto", e);
        }
    }

    // Lista todos os produtos
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();

    }

    // Encontra um produto por ID
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
}