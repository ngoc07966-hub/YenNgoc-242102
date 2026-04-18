using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YenNgoc_242102.Models;
namespace YenNgoc_242102.Controllers
{
    public class theloaitinController : Controller
    {
        // GET: theloaitin
        DatatintucDataContext data = new DatatintucDataContext(System.Configuration.ConfigurationManager.ConnectionStrings["TintucConnectionString"].ConnectionString);
        public ActionResult Index()
        {
            var All_Loaitin = from tt in data.Theloaitins select tt;
            return View(All_Loaitin);
        }
        //Hàm Details truyền dữ liệu sang trang Details.aspx
        //Với tham số được truyền là IDLoai (trong bảng Theloaitin)
        public ActionResult Details(int ? id)
        {
            var Details_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            return View(Details_tin);
        }
        //Hàm Create (get )tạo khung cho người sử dụng nhập liệu
        public ActionResult Create()
        {
            return View();
        }
        //Hàm Create(Post) xử lý dữ liệu được chuyền về từ trang Create.aspx
        //và trả về kết quả
        [HttpPost]
        public ActionResult Create(FormCollection collection, Theloaitin ltin)
        {
            // Tạo biến CB_Loaitin và gán giá trị của người dùng nhập vào từ
            //form trong trang Create.aspx
            var CB_Loaitin = collection["Tentheloai"];
            //Nếu CB_Loaitin có giá trị == null ( để trống )
            if (string.IsNullOrEmpty(CB_Loaitin))
            {
                ViewData["Loi"] = " Thể loại Tin không được để trống ";
            }
            else
            {
                ltin.Tentheloai = CB_Loaitin;
                data.Theloaitins.InsertOnSubmit(ltin);
                //Thực hiện tạo mới
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        // GET:Hàm Edit(get) t ruyền thông số của đối tượng sang trang Edit.aspx
        //Với thông số là id.
        public ActionResult Edit(int id)
        {
            var EB_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(EB_tin);
        }
        // POST: Hàm Edit(post) thực hiện update dữ liệu từ trang Edit.aspx
        //khi Click Submits
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            // Tạo một biến Ltin gán với đối tượng có id=id truyền vào
            var Ltin = data.Theloaitins.First(m => m.IDLoai == id);
            var E_Loaitin = collection["Tentheloai"];
            //vì ta sửa đối tượng lên Id của biến Ltin = Id chuyền vào .
            Ltin.IDLoai = id;
            // Nếu người dùng để phần Loại Tin trống báo lỗi
            if (string.IsNullOrEmpty(E_Loaitin))
            {
                ViewData["Loi"] = "Thể loại tin không được để trống ";
            }
            // Ngược lại gán các trường của biến Ltin bằng các giá trị
            //của người dùng nhập vào
            else
            {
                Ltin.Tentheloai = E_Loaitin;
                // Thực hiện updat
                UpdateModel(Ltin);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        // GET: Hàm Delete ( get ) đưa dữ liệu của đối tượng cần xóa lên trang Delete
        // cho người dùng xem. Tham số truyền vào là id
        public ActionResult Delete(int id)
        {
            var D_tin = data.Theloaitins.First(m => m.IDLoai == id);
            return View(D_tin);
        }
        // POST: Hàm Delete ( post ) thực thi lệnh xóa đối tượng khi người dùng
        // click xóa từ trang Delete.aspx . Với tham số Id
        [HttpPost]
        
        public ActionResult Delete(int id, FormCollection collection)
        {
            // Tạo biến D_Tin gán với dối dượng có ID bằng với ID tham số
            var D_tin = data.Theloaitins.Where(m => m.IDLoai == id).First();
            //xóa
            data.Theloaitins.DeleteOnSubmit(D_tin);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }

}
