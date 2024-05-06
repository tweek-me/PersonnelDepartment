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

    [HttpPost("/posts/savePost")]
    public Result SavePost([FromBody] PostBlank postBlank)
    {
        return _departmentService.SavePost(postBlank);
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

    [HttpPost("/posts/removePost")]
    public Result RemovePost([FromBody] Guid id)
    {
        return _departmentService.RemovePost(id);
    }
}
