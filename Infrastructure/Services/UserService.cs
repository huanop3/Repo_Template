using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_base.Helper;
using web_api_base.Models.dbebay;
using web_api_base.Models.ViewModel;

public interface IUserService
{
    Task<dynamic> Register(dynamic user);
    Task<ActionResult> Login(UserLoginVM userLogin);
}
public class UserService : IUserService
{
    public IUnitOfWork _unitOfWork { get; }
    public JwtAuthService _jwtService { get; }
    public UserService(IUnitOfWork unitOfWork, JwtAuthService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }
    public async Task<ActionResult> Login(UserLoginVM userlogin)
    {
        //kiem tr user trong db
        User? userDb = await _unitOfWork._userRepository.SingleOrDefaultAsync(n => n.Username == userlogin.Account || n.Email == userlogin.Account);
        if (userDb != null && PasswordHelper.VerifyPassword(userlogin.Password,userDb.PasswordHash)){
            //dang nhap thanh cong
            //tao token cho user
            UserLoginResultVM userLoginResult = new UserLoginResultVM();
            userLoginResult.Account = userlogin.Account;
            userLoginResult.AccessToken = _jwtService.GenerateToken(userDb);
            var resOk = new HTTPResponseClient<UserLoginResultVM>()
            {
                StatusCode = 200,
                Message = "Login success",
                DateTime = DateTime.Now,
                Data = userLoginResult
            };
            return new OkObjectResult(resOk);
        }
        else
        {
            //khong tim thay user hoac mat khau khong dung
            //tra ve 401 Unauthorized
            var resBad = new HTTPResponseClient<UserLoginResultVM>(){
                StatusCode = 400,
                Message = "Login fail",
                DateTime = DateTime.Now,
                
            };
            //dang nhap khong thanh cong
            return new BadRequestResult();
        }


    }

    public Task<dynamic> Register(dynamic user)
    {
        throw new NotImplementedException();
    }
    //Generate a token for the user
}