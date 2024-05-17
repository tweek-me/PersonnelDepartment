using Microsoft.AspNetCore.Mvc;
using PersonnelDepartment.Domain.Posts;
using PersonnelDepartment.Infrastructure;
using PersonnelDepartment.Services.Departments;
using PersonnelDepartment.Tools.Results;

namespace PersonnelDepartment.Controllers;
public class PostsController : BaseController
{
    private readonly IDepartmentService _departmentService;

    public PostsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    public record SavePostRequest(PostBlank PostBlank);

    [HttpPost("/posts/savePost")]
    public Result SavePost([FromBody] SavePostRequest request)
    {
        return _departmentService.SavePost(request.PostBlank);
    }

    [HttpGet("/posts/get")]
    public Post? GetPost(Guid postId)
    {
        return _departmentService.GetPost(postId);
    }

    [HttpGet("/posts/getPosts")]
    public Post[] GetPosts(Guid departmentId)
    {
        return _departmentService.GetPosts(departmentId);
    }

    public record RemovePostRequest(Guid PostId);

    [HttpPost("/posts/removePost")]
    public Result RemovePost([FromBody] RemovePostRequest request)
    {
        return _departmentService.RemovePost(request.PostId);
    }
}
