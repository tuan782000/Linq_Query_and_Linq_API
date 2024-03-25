using System;
using System.Linq;

namespace Linq;

internal class Program
{
    // Linq (Language Integrated Query): Ngôn ngữ truy vấn tích hợp
    // SQL - nó là sql nhưng được C# cải tiến lại
    // Nguồn dữ liệu: IEnumerable, IEnumerable<T> (Array, List, Task, Queue ...)
    //                SQL, XML

    public class Product
    {
        public int ID {set; get;}
        public string Name {set; get;}         // tên
        public double Price {set; get;}        // giá
        public string[] Colors {set; get;}     // các màu sắc
        public int Brand {set; get;}           // ID Nhãn hiệu, hãng
        public Product(int id, string name, double price, string[] colors, int brand)
        {
            ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        }
        // Lấy chuỗi thông tin sản phẳm gồm ID, Name, Price
        override public string ToString()
        => $"{ID,3} {Name, 12} {Price, 5} {Brand, 2} {string.Join(",", Colors)}";
    }

    public class  Brand {
        public string Name {set; get;}
        public int ID {set; get;}
    }
    static void Main(string[] args)
    {
        Console.InputEncoding = System.Text.Encoding.UTF8;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Tạo ra 1 producr
        // Product p = new Product( 1, "ABC", 1000, new string[] {"Xanh", "Đỏ"}, 2);
        // Console.WriteLine($"Product: {p.ToString()}"); 
        // phải có toString mới in ra được, mà phương thức toString tự động gọi cho nên là viết gọn lại.
        // Phương thức to String sẽ in ra các thông tin sau ID = id; Name = name; Price = price; Colors = colors; Brand = brand;
        // Console.WriteLine($"Product: {p}");

        // Tạo ra danh sách các brands
        var brands = new List<Brand>() {
            new Brand{ID = 1, Name = "Công ty AAA"},
            new Brand{ID = 2, Name = "Công ty BBB"},
            new Brand{ID = 4, Name = "Công ty CCC"},
        };
        // Danh sách các sản phẩm
        var products = new List<Product>()
        {
            new Product(1, "Bàn trà",    400, new string[] {"Xám", "Xanh"},         2),
            new Product(2, "Tranh treo", 400, new string[] {"Vàng", "Xanh"},        1),
            new Product(3, "Đèn trùm",   500, new string[] {"Trắng"},               3),
            new Product(4, "Bàn học",    200, new string[] {"Trắng", "Xanh"},       1),
            new Product(5, "Túi da",     300, new string[] {"Đỏ", "Đen", "Vàng"},   2),
            new Product(6, "Giường ngủ", 500, new string[] {"Trắng"},               2),
            new Product(7, "Tủ áo",      600, new string[] {"Trắng"},               3),
        };

        // Lấy ra các sản phẩm có giá bằng 400
        // From - where - OrderBy - Having - GroupBy - Select
        var query = from p in products 
                    where p.Price == 400 
                    select p; 
        // đây là câu truy vấn linq thực hiện trên bảng product
        // dùng vòng lặp in kết quả ra
        foreach(var product in query) {
            Console.WriteLine(product);
        }

        // Giải thích: products đại diện cho nguôn dữ liệu - chúng ta có thể dữ liệu từ database thông qua api - của đối tượng Product - tiếp tục thực hiện truy vấn và trả về kết quả và lưu vào query

        // Linq mở rộng ra cho các IEnumerable như là Array, List có thêm những phương thúc lọc, lấy, những phương thức này phục vụ cho truy vấn biến đổi dữ liệu từ nguồn

        // Select - nhận delegate - select này c# viết sẵn chỉ lấy ra dùng là được - dữ liệu trả về trùng với kiểu dữ liệu delegate

        // products là một IEnumerable hoặc IQueryable đại diện cho một danh sách các đối tượng.
        //.Select() là một phương thức LINQ, được sử dụng để chọn và chuyển đổi các phần tử trong danh sách ban đầu thành các phần tử mới, theo một quy tắc nhất định.
        // (p) => { return p.Name; }: Đây là biểu thức lambda. p là một biến đại diện cho mỗi phần tử trong products. Biểu thức này chọn thuộc tính Name từ mỗi đối tượng và trả về nó.
        var kq = products.Select(
            (p) => {
                return p.Name; // lúc này nhiệm select là chỉ lấy ra mỗi cột Name
            }
        );

        foreach (var item in kq)
        {
            Console.WriteLine(item);
        }

        var kq2 = products.Select(
            (p) => {
                return p.Price; // lúc này nhiệm select là chỉ lấy ra mỗi cột Price
            }
        );

        foreach (var item in kq2)
        {
            Console.WriteLine(item);
        }

        // Sự kết hợp

        var kq3 = products.Select(
            (p) => {
                return p.Name + " " + "(" + p.Price + ")"; // lúc này nhiệm select là chỉ lấy ra mỗi cột Price
            }
        );

        foreach (var item in kq3)
        {
            Console.WriteLine(item);
        }

        var kq4 = products.Select(
            (p) => { 
                return new { // tạo ra đối tượng kiểu vô danh
                    Tên = p.Name,
                    Giá = p.Price
                };
            }
        );

        foreach (var item in kq4)
        {
            Console.WriteLine(item);
        }

        System.Console.WriteLine("-----");

        // Where - nhận delegate - và trả vê kiểu bool - trả về true phần tử đó được lấy ra

        var kq5 = products.Where(
            (p) => { // Contains - hàm - bao gồm
                return p.Name.Contains("tr"); // phần tử nào thỏa điều kiện sẽ được lấy ra
                // tên phải bao gồm tr - lúc này c# tự viết sinh ra linq phù hợp để kiểm tra
            }
        );
        foreach (var item in kq5)
        {
            Console.WriteLine(item);
        }

        System.Console.WriteLine("-----");

        var kq6 = products.Where(
            (p) => { 
                // Lấy ra những sản phẩm có brand là 2
                return p.Brand == 2;
                
            }
        );
        foreach (var item in kq6)
        {
            Console.WriteLine(item);
        }

        System.Console.WriteLine("-----");

        var kq7 = products.Where(
            (p) => { 
                // Lấy ra những sản phẩm có giá lớn hơn hoặc bằng 200 và nhỏ hơn hoặc bằng 300
                return p.Price >= 200 && p.Price <= 300;
            }
        );
        foreach (var item in kq7)
        {
            Console.WriteLine(item);
        }

        // SelectMany
        Console.WriteLine("---------------");
        // Đối với các giá trị như danh sách new string[] {"Xám", "Xanh"}
        // thì không thể dùng Select bình thường được mà phải sử dụng SelectMany thì mới lấy ra được hết

        var kq8 = products.SelectMany(
            (p) => { 
                // Lấy ra tập hợp các giá trị Color từ mỗi đối tượng Product
                return p.Colors;
            }
        );
        foreach (var item in kq8)
        {
            Console.WriteLine(item);
        }

        // Min Max Sum Average

        int [] numbers = {1,2,3,4,5,6,7,8,9};

        // Tìm ra số lớn nhất
        Console.WriteLine("-------Lớn Nhất--------");
        System.Console.WriteLine(numbers.Max());

        // Tìm ra số nhỏ nhất

        Console.WriteLine("-------Nhỏ Nhất--------");
        System.Console.WriteLine(numbers.Min());

        // Tính Tổng
        Console.WriteLine("-------Tổng--------");
        System.Console.WriteLine(numbers.Sum());

        // Tính Tổng
        Console.WriteLine("-------TBC--------");
        System.Console.WriteLine(numbers.Average());

        // Tìm ra số chẵn - lớn nhất (số chẵn là số % 2 = 0)
        Console.WriteLine("-------Tìm ra số chẵn - lớn nhất--------");
        System.Console.WriteLine(numbers.Where(n => n % 2 == 0).Max());

        // tính tổng các số chẵn
        Console.WriteLine("-------tính tổng các số chẵn--------");
        System.Console.WriteLine(numbers.Where(n => n % 2 == 0).Average());

        // Lấy ra giá nhỏ nhất trong danh sách sản phẩm
         Console.WriteLine("-------sản phẩm có giá nhỏ nhất--------");
         Console.WriteLine(products.Min( p => p.Price));

        // Lấy ra trung bình giá
        Console.WriteLine("-------trung bình giá--------");
        Console.WriteLine(products.Average( p => p.Price));

        // Join Bảng
        // Kết hợp các nguồn dữ liệu để lấy ra các ngồn dữ liệu phù hợp
        // thí dụ giờ chỉ muốn lấy ra tên sản phẩm và tên nhà cung cấp.
        // Nhưng mà trong bảng sản phẩm chỉ có tên sản phẩm - bảng brand có tên brand.
        // Buộc ta phải join 2 bảng lại với nhau

        // logic như sau: Lấy ra id sản phẩm - tên - id của brand trong bảng product. Sau đó đem thông tin Brand Id tra và bảng Brand khớp với Id bảng brand thì sẽ soi ra được tên của brand đó

        var kq9 = products.Join(brands, p => p.Brand, b => b.ID, (p,b) => {
            return new {
                Ten = p.Name,
                Thuonghieu = b.Name
            };
        });

        // những thằng product mặc dù có Brand trong bảng product - nhưng bảng Brand thì lại ko có Brand này thành ra không hiển thị 

        foreach (var item in kq9)
        {
            Console.WriteLine(item);
        }

        // Group Join - Hoạt động tương tự Join - Tuy nhiên phần tử trả về là 1 nhóm - mà được nhóm lại theo cái nguồn ban đầu.

        // Có danh sách Các brands - với mỗi cái thương hiệu thì liệt kê những sản phẩm thuộc thương hiệu đó ra

        var kq10 = brands.GroupJoin(products, b => b.ID, p => p.Brand, (brand, pros) => {
            return new {
                Thuonghieu = brand.Name,
                Cacsanpham = pros
            };
        });

        foreach(var gr in kq10) {
            Console.WriteLine(gr.Thuonghieu);
            foreach(var p in gr.Cacsanpham) {
                Console.WriteLine(p);
            }
        }

        // GroupJoin(tham số 1: nguồn của các phần tử nằm trong nhóm, tham số 2: dữ liệu ban đầu mang ra tạo nhóm, tham số 3: t-s-1 có phần tử trùng với dữ liệu t-s-2 thì từ đó đưa vào 1 nhóm, tham số 4: delegate (có 2 tham số nữa))

        /*
            bảng_bị_Join.GroupJoin(bảng_đem_join, dữ_liệu_bảng_bị, dữ_liệu_bảng_đem, 
                (cụ_thể_DLBB, cụ_thể_DLBĐ) => {
                    return new {
                        Thuonghieu = cụ_thể_DLBB.Name,
                        Cacsanpham = cụ_thể_DLBĐ
                    }
                })
        */ 
        // Take: Lấy ra 1 số sản phẩm đầu tiên Take(truyền_số_sản_phẩm_muốn_lấy)
        Console.WriteLine("-------Lấy ra một vài phần tử đầu tiên--------");
        products.Take(2).ToList().ForEach(p => Console.WriteLine(p));

        // Skip: bỏ qua những phần tử đầu tiên - và lấy những phần tử còn lại
        Console.WriteLine("-------bỏ qua phần tử đầu tiên--------");
        products.Skip(2).ToList().ForEach(p => Console.WriteLine(p));

        // OrderBy -- sắp xếp theo giá trị tăng dần / OrderByDescending -- sắp xếp theo giá trị giảm dần
        Console.WriteLine("-------OrderBy--------");
        products.OrderBy(p => p.Price).ToList().ForEach(p => System.Console.WriteLine(p));
        // sắp xếp sản phẩm theo giá tăng dần. chuyển nó thành danh sách. Sau đó dùng ForEach duyệt qua. lấy ra từng những sản phẩm

        Console.WriteLine("-------OrderByDescending--------");
        products.OrderByDescending(p => p.Price).ToList().ForEach(p => System.Console.WriteLine(p));

        // Reverse
        Console.WriteLine("-------Chưa Đảo ngược--------");
        numbers.ToList().ForEach(n => System.Console.WriteLine(n));
        numbers.Reverse().ToList().ForEach(n => System.Console.WriteLine(n));

        // Group By: Nhóm mỗi phần tử theo 1 dữ liệu nào đó
        Console.WriteLine("-------Nhóm sản phẩm theo giá--------");

        var result = products.GroupBy(p => p.Price);

        foreach(var group in result) {
            Console.WriteLine(group.Key);
            foreach(var p in group) {
                Console.WriteLine(p);
            }
        }

        Console.WriteLine("-------Nhóm sản phẩm theo brand--------");
        var result01 = products.GroupBy(p => p.Brand);

        foreach(var group in result01) {
            Console.WriteLine(group.Key);
            foreach(var p in group) {
                Console.WriteLine(p);
            }
        }

        // Distinct: bỏ các phần tử có cùng giá trị
        Console.WriteLine("-------bỏ các phần tử có cùng giá trị, cụ thể là màu--------");
        products.SelectMany(p => p.Colors)
                .Distinct()
                .ToList()
                .ForEach(mau => System.Console.WriteLine(mau));

        // Single  / SignleOrDefault
        var result02 = products.Single(p => p.Price == 600);
        System.Console.WriteLine(result02); // chỉ trả về 1 thì không lỗi - trả về nhiều hơn 1 lỗi - hoặc không tìm thấy cũng lỗi.
        var result03 = products.SingleOrDefault(p => p.Price == 600);
        if(result03 != null)
        System.Console.WriteLine(result03); 

        // Any - trả về true nếu thỏa mãn kiểu logic nào đó
        Console.WriteLine("-------Trả lời true/false về một điều kiện nào đó--------");
        var result04 = products.Any(p => p.Price == 600);
        System.Console.WriteLine(result04); // true

        var result05 = products.Any(p => p.Price == 700);
        System.Console.WriteLine(result05);// false vì hiện tại không có sản phẩm giá 700

        // All - trả về true hoặc false
        Console.WriteLine("-------Trả lời true/false về một list thỏa điều kiện hay không--------");
        var result06 = products.All(p => p.Price >= 200);
        System.Console.WriteLine(result06);// true vì hiện tại tất cả đều lớn hơn hoặc bằng 200

        var result07 = products.All(p => p.Price >= 300);
        System.Console.WriteLine(result07);// false vì hiện tại có 1 vài sản phẩm bằng 200

        // Count đếm số lượng - đếm tất cả sản phẩm có trong product
        Console.WriteLine("-------đếm tất cả sản phẩm có trong product--------");
        var result08 = products.Count();
        System.Console.WriteLine(result08); // 7

        // đếm kèm điều kiện
        // đếm có bao nhiêu sản phẩm lớn hơn hoặc bằng 300
        var result09 = products.Count(p => p.Price >= 300);
        System.Console.WriteLine(result09); // 6

        // đếm có bao nhiêu sản phẩm bằng 400
        var result10 = products.Count(p => p.Price == 400);
        System.Console.WriteLine(result10); // 6

        System.Console.WriteLine("----Bài tập----");
        // In ra tên sản phẩm, thương hiệu, giá từ 300 - 400
        // giá giảm dần

        // Cách này chưa được
        // var rsp =  products.Where(p => p.Price >= 300 && p.Price <= 400 ).OrderByDescending(p => p.Price);
        // var result11 = brands.GroupJoin(rsp, b => b.ID, p => p.Brand, (
        //     (b, p) => {
        //         return new {
        //             ThuongHieu = b.Name,
        //             SanPham = p,
        //         };
        //     }
        // ));
        // foreach (var item in result11)
        // {
        //     System.Console.WriteLine(item.ThuongHieu);
        //     foreach (var item2 in item.SanPham)
        //     {
        //         System.Console.WriteLine(item2);
        //     }
        // }

        products.Where(p => p.Price >= 300 && p.Price <= 400)
                .OrderByDescending(p => p.Price)
                .Join(brands, p => p.Brand, b => b.ID, (
                    (sp,th) => new {
                        TenSP = sp.Name,
                        ThuongHieu = th.Name,
                        Gia = sp.Price
                    }
                ))
                .ToList()
                .ForEach(info => System.Console.WriteLine($"{info.TenSP, 15} - {info.ThuongHieu, 15} - {info.Gia}"));


        /*
            1/ Xác định nguồn: 
            
            from ten_phan_tu in IEnumerable // với mỗi phần tử tên phần tử thuộc đối tượng IEnumerable

                ... JOIN - where, orderby,... let ten_bien = ??? (bieu thuc tinh toan)

            2/ Chỉ thị Lấy dữ liệu: Select - GroupBy,...  - dữ liệu được lấy ra căn cứ theo phần tử nó đã chọn từ nguồn

            Linq: from -> where -> orderby -> Having -> groupby -> select
        */
        System.Console.WriteLine("-------");

        // products - IEnumerable - nó cũng là nguồn - ten_phan_tu thì mình tự đặt
        var qr = from p in products select p.Name; // sau select thương trả về là đối tượng tính toán dự trên ten_phan_tu
        // qr lúc này đã thành IEnumerable có kiểu là chuỗi
        
        qr.ToList().ForEach(name => System.Console.WriteLine(name));
        
        // foreach (var name in qr) {
        //     System.Console.WriteLine(name); // Lấy ra được danh sách các tên sản phẩm
        // }

        // Hiển thị tên và giá bằng linq

        System.Console.WriteLine("-------");
        var qr2 = from p in products select $"{p.Name} {p.Price}";
        qr2.ToList().ForEach(p => System.Console.WriteLine(p));

        // Trường hợp phức tạp hơn muốn trả về kiểu vô danh

        System.Console.WriteLine("-------");
        var qr3 = from p in products select new {
            Ten = p.Name,
            Gia = p.Price,
            BrandId = p.Brand
        };
        // có 2 cách lấy ra danh sách - 1 là dùng vòng lặp - 2 là convert nó sang Tolist sau đó, ForEach
        qr3.ToList().ForEach(p => System.Console.WriteLine(p));

        System.Console.WriteLine("-------");
        // where trả true thì select mới chạy với những thằng true
        var qr4 = from product in products where product.Price == 400 select new {
            TenSP = product.Name,
            Gia = product.Price
        };
        qr4.ToList().ForEach(p => System.Console.WriteLine($"{p.TenSP} {p.Gia}"));

        // Nhỏ hơn hoặc bằng 400
        System.Console.WriteLine("-------");
        var qr5 = from product in products where product.Price <= 400 select new {
            TenSP = product.Name,
            Gia = product.Price
        };
        qr5.ToList().ForEach(p => System.Console.WriteLine($"{p.TenSP} {p.Gia}"));


        // Sử dụng nhiều nguồn cùng 1 lúc ( 2 cái from trở lên)
        // lấy ra sản phẩm = 500, có màu xanh
                System.Console.WriteLine("-------");

        var qr6 = from product in products
                  from color in product.Colors
                  where product.Price <= 500 && color == "Xanh"
                  select new {
                    Ten = product.Name,
                    Gia = product.Price,
                    Mau = product.Colors
                  };
        qr6.ToList().ForEach(p => System.Console.WriteLine($"{p.Ten} - {p.Gia} - {string.Join(",", p.Mau)}"));


        System.Console.WriteLine("---tăng----");

        var qr7 = from product in products
                  from color in product.Colors
                  where product.Price <= 500 && color == "Xanh"
                  orderby product.Price
                  select new {
                    Ten = product.Name,
                    Gia = product.Price,
                    Mau = product.Colors
                  };
        qr7.ToList().ForEach(p => System.Console.WriteLine($"{p.Ten} - {p.Gia} - {string.Join(",", p.Mau)}"));

        System.Console.WriteLine("---giảm----");

        var qr8 = from product in products
                  from color in product.Colors
                  where product.Price <= 500 && color == "Xanh"
                  orderby product.Price descending
                  select new {
                    Ten = product.Name,
                    Gia = product.Price,
                    Mau = product.Colors
                  };
        qr8.ToList().ForEach(p => System.Console.WriteLine($"{p.Ten} - {p.Gia} - {string.Join(",", p.Mau)}"));

        // Nhóm sản phẩm theo giá

                System.Console.WriteLine("---Nhóm theo giá----");

        var qr9 = from p in products
                  group p by p.Price;

        qr9.ToList().ForEach(group => {
            System.Console.WriteLine(group.Key);
            group.ToList().ForEach(p => System.Console.WriteLine(p));
        });

        System.Console.WriteLine("---Nhóm theo giá----");
        
        var qr10 = from p in products
                  group p by p.Price into gr
                  orderby gr.Key
                  select gr;

        qr10.ToList().ForEach(group => {
            System.Console.WriteLine(group.Key);
            group.ToList().ForEach(p => System.Console.WriteLine(p));
        });


        // Đối tượng
        // Giá = giá sản phẩm
        // Các sản phẩm : 1 nhóm các sản phẩm = với thuộc tính giá
        // Số lượng: có bao nhiêu sản phẩm cùng giá
        System.Console.WriteLine("----sử dụng biến let - thực hiện các phức tạp----");
        /*
            Ý tưởng: 
            - từ "products" ta lấy ra được từng cái "p" - mỗi "p" tương ứng với mỗi "product".
            - Sau đó nhóm p theo giá - và đặt tên là gr - lúc này là mỗi nhóm là 1 gr - Lấy giá làm thành 1 điểm chung "Key", mỗi giá là mỗi Key khác nhau
            - p nào mà có giá trùng với Key thì được vào Key đó
            - Sắp xếp các Key theo thứ tự từ thấp đến cao
            - gr.Count đếm số lượng của mỗi gr
            - Cuối cùng tạo ra đối tượng mới ánh xạ thông tin Gia = gr.Key, CacSanPham = gr.ToList() - hiển danh sách của mỗi gr, Soluong = sl số lượng đếm được của mỗi gr

            - (1) Từ "products", chúng ta lấy ra từng đối tượng "p", trong đó mỗi "p" tương ứng với một "product".
            - (2) Sau đó, chúng ta nhóm các đối tượng "p" theo giá và đặt tên cho mỗi nhóm là "gr". Ở đây, mỗi nhóm "gr" được tạo ra dựa trên giá của các sản phẩm, và "Key" được sử dụng để đại diện cho giá, 
            - (2) Các đối tượng "p" nào có giá trùng với "Key" (giá trong nhóm), sẽ được đưa vào nhóm đó.
            - (3) Các nhóm được sắp xếp theo thứ tự tăng dần của giá (tức là từ giá thấp nhất đến giá cao nhất).
            - (4) "gr.Count()" đếm số lượng đối tượng trong mỗi nhóm "gr",
            - (5) Cuối cùng, chúng ta tạo ra một đối tượng mới trong mỗi nhóm, ánh xạ thông tin về giá (Gia), danh sách các sản phẩm trong nhóm (CacSanPham), và số lượng sản phẩm trong nhóm (Soluong).
        */
        var qr20 = from p in products 
                   group p by p.Price into gr 
                   orderby gr.Key
                   let sl = "Số lượng là " + gr.Count()
                   select new {
                    Gia = gr.Key,
                    CacSanPham = gr.ToList(),
                    Soluong = sl
                };
        
        // dùng forEach duyệt qua đối tuọng
        // Dựa trên danh sách qr20, bạn sử dụng phương thức ForEach để lặp qua từng phần tử i trong danh sách.
        qr20.ToList().ForEach(i => {
            System.Console.WriteLine(i.Gia);
            System.Console.WriteLine(i.Soluong);
            i.CacSanPham.ForEach(p => System.Console.WriteLine(p));
        });

        // Trong mỗi lần lặp, bạn in ra giá (Gia) và số lượng (Soluong) của nhóm sản phẩm đó. Sau đó, bạn sử dụng một vòng lặp trong ForEach để lặp qua từng sản phẩm trong danh sách CacSanPham, và in ra thông tin của từng sản phẩm.

        // Vì CacSanPham là một danh sách các sản phẩm, bạn cần sử dụng một vòng lặp nữa để lặp qua từng sản phẩm trong danh sách này.

        System.Console.WriteLine("------");
        // Đề: Liệt kê ra các tên nhà cung cấp - tên sản phẩm - giá
        var qr21 = from product in products 
                   join brand in brands 
                   on product.Brand equals brand.ID
                   select new {
                    ten = product.Name,
                    gia = product.Price,
                    thuonghieu = brand.Name
                   };

                   // lấy dữ liệu từ products ra thành các product sau đó join tới bảng brands lấy ra các brand, 2 bảng này có điểm chung là "Brand (Brand này thực ra là Id của brands) và Id". Từ đó lấy ra Ten và Gia trong bang Product, lấy ra brand Name trong bảng brands

        qr21.ToList().ForEach(o => {
            System.Console.WriteLine($"{o.ten, 10}, {o.gia, 5}, {o.thuonghieu, 10}");
        });

        // Được xem inner Join -- Mỗi cái product phải khớp nối với brand thì mới lấy ra -- còn lại nếu product không khớp với brand hoặc brand không khớp Product thì không lấy ra.

        // Left Join
        System.Console.WriteLine("---Sử dụng Left Join-");
        /*
            Tất cả các bản ghi từ bảng bên trái (A) sẽ được lấy hết.
            Từ bảng bên phải (B), chỉ những bản ghi khớp với điều kiện join sẽ được lấy ra, và các cột tương ứng với các bản ghi không khớp sẽ được điền giá trị NULL.
        */
        var qr22 = from product in products 
                   join 
                   brand in brands 
                   on product.Brand equals brand.ID 
                   into t from b in t.DefaultIfEmpty()
                   select new {
                        ten = product.Name,
                        gia = product.Price,
                        thuonghieu = (b != null) ? b.Name : "No Brand"
                   };
            qr22.ToList().ForEach(o => {
                System.Console.WriteLine($"{o.ten, 10}, {o.gia, 5}, {o.thuonghieu, 10}");
            });

        /*
            Đoạn join 
                   brand in brands 
                   on product.Brand equals brand.ID 
            khới nối Product.

            Sẽ dùng Biến t là biến tạm chứa nguyên đoạn "join 
                   brand in brands 
                   on product.Brand equals brand.ID "

            into t giúp làm điều đó

            from xác định nguồn dữ liệu thứ 2 
            "b" đang lưu trong "t" - với mỗi product có b lưu trong t - Nếu product không lưu được b trong t sẽ trả về null

            chính vì vậy t.DefaultIfEmpty() -- trả về giá trị nếu tìm thấy nếu không b sẽ là null

            từ đó xác định phần sẽ join vào được hóa thành b - b.Name để tham chiếu bảng brands để lấy ra thông tin của brand Name - b có thể là null nên phải sử dụng toán tử 3 ngôi nếu "b != null" b. name có tên trả về name không trả về null
        */ 

        // Left Join
        System.Console.WriteLine("---Sử dụng Left Join-");
        var qr23 = from brand in brands 
                   join 
                   product in products 
                   on brand.ID equals product.Brand 
                   into t from b in t.DefaultIfEmpty()
                   select new {
                        thuonghieu = brand.Name,
                        Ten = (b != null) ? b.Name : "No Name",
                        Gia = (b != null) ? b.Price : 0.0
                   };

        qr23.ToList().ForEach(i => {
            System.Console.WriteLine($"{i.thuonghieu}, {i.Ten}, {i.Gia}");
        });
    }
}
