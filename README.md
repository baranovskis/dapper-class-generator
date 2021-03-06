## Introduction :memo:
**DapperClassGenerator** is a small tool for POCOs (Plain Old Class Object) generation using T4 text templates.

Features
--------
- Supports [Dapper.Contrib](https://github.com/StackExchange/Dapper/tree/master/Dapper.Contrib)
- Supports [FluentValidation](https://fluentvalidation.net/)
- Very fast - single query collects all data needed for generation
- Easy to change - just edit T4 template & process it

## Synopsis :mag:
```sh
DapperClassGenerator 1.0.0
Copyright (C) 2021 DapperClassGenerator

  -c, --connection    Required. Set MSSQL connection string.

  -n, --namespace     Required. Set class namespace.

  -o, --output        Generated file path.

  --annotations       Use Dapper.Contrib data annotations.

  --validator         Generate validator class (FluentValidation).

  --help              Display this help screen.

  --version           Display version information.
```

## Usage :construction_worker:
```sh
>  ./DapperClassGenerator.exe -c "Server=<IP>;Database=StackOverflow2010;User Id=<Login>;Password=<Password>;" -n StackOverflow.Domain -o "D:/MyProject/src/MyProject.Domain" --annotations --validator
```

## Generated POCOs
```csharp
/* 
 * This file is automatically generated by DapperClassGenerator tool.
 * Do not modify this file -- YOUR CHANGES WILL BE ERASED!
 */
using System;
using Dapper.Contrib.Extensions;

// ReSharper disable All
namespace StackOverflow.Domain
{
    [Table("[dbo].Badges")]
    public class Badge
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
    }

    [Table("[dbo].Comments")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public int? Score { get; set; }
        public string Text { get; set; }
        public int? UserId { get; set; }
    }

    [Table("[dbo].LinkTypes")]
    public class LinkType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }

    [Table("[dbo].PostLinks")]
    public class PostLink
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int PostId { get; set; }
        public int RelatedPostId { get; set; }
        public int LinkTypeId { get; set; }
    }

    [Table("[dbo].Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int? AcceptedAnswerId { get; set; }
        public int? AnswerCount { get; set; }
        public string Body { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? CommentCount { get; set; }
        public DateTime? CommunityOwnedDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int? FavoriteCount { get; set; }
        public DateTime LastActivityDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public string LastEditorDisplayName { get; set; }
        public int? LastEditorUserId { get; set; }
        public int? OwnerUserId { get; set; }
        public int? ParentId { get; set; }
        public int PostTypeId { get; set; }
        public int Score { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public int ViewCount { get; set; }
    }

    [Table("[dbo].PostTypes")]
    public class PostType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
    }

    [Table("[dbo].Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string AboutMe { get; set; }
        public int? Age { get; set; }
        public DateTime CreationDate { get; set; }
        public string DisplayName { get; set; }
        public int DownVotes { get; set; }
        public string EmailHash { get; set; }
        public DateTime LastAccessDate { get; set; }
        public string Location { get; set; }
        public int Reputation { get; set; }
        public int UpVotes { get; set; }
        public int Views { get; set; }
        public string WebsiteUrl { get; set; }
        public int? AccountId { get; set; }
    }

    [Table("[dbo].Votes")]
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public int? BountyAmount { get; set; }
        public int VoteTypeId { get; set; }
        public DateTime CreationDate { get; set; }
    }

    [Table("[dbo].VoteTypes")]
    public class VoteType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
```

## Generated Validator Class
```csharp
/* 
 * This file is automatically generated by DapperClassGenerator tool.
 * Do not modify this file -- YOUR CHANGES WILL BE ERASED!
 */
using FluentValidation;

// ReSharper disable All
namespace StackOverflow.Domain
{
    public class BadgeValidator : AbstractValidator<Badge>
    {
        public BadgeValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.Name).NotEmpty();
            RuleFor(expr => expr.Name).MaximumLength(40);
            RuleFor(expr => expr.UserId).NotEmpty();
            RuleFor(expr => expr.Date).NotEmpty();
        }
    }

    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.CreationDate).NotEmpty();
            RuleFor(expr => expr.PostId).NotEmpty();
            RuleFor(expr => expr.Text).NotEmpty();
            RuleFor(expr => expr.Text).MaximumLength(700);
        }
    }

    public class LinkTypeValidator : AbstractValidator<LinkType>
    {
        public LinkTypeValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.Type).NotEmpty();
            RuleFor(expr => expr.Type).MaximumLength(50);
        }
    }

    public class PostLinkValidator : AbstractValidator<PostLink>
    {
        public PostLinkValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.CreationDate).NotEmpty();
            RuleFor(expr => expr.PostId).NotEmpty();
            RuleFor(expr => expr.RelatedPostId).NotEmpty();
            RuleFor(expr => expr.LinkTypeId).NotEmpty();
        }
    }

    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.Body).NotEmpty();
            RuleFor(expr => expr.CreationDate).NotEmpty();
            RuleFor(expr => expr.LastActivityDate).NotEmpty();
            RuleFor(expr => expr.LastEditorDisplayName).MaximumLength(40);
            RuleFor(expr => expr.PostTypeId).NotEmpty();
            RuleFor(expr => expr.Score).NotEmpty();
            RuleFor(expr => expr.Tags).MaximumLength(150);
            RuleFor(expr => expr.Title).MaximumLength(250);
            RuleFor(expr => expr.ViewCount).NotEmpty();
        }
    }

    public class PostTypeValidator : AbstractValidator<PostType>
    {
        public PostTypeValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.Type).NotEmpty();
            RuleFor(expr => expr.Type).MaximumLength(50);
        }
    }

    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.CreationDate).NotEmpty();
            RuleFor(expr => expr.DisplayName).NotEmpty();
            RuleFor(expr => expr.DisplayName).MaximumLength(40);
            RuleFor(expr => expr.DownVotes).NotEmpty();
            RuleFor(expr => expr.EmailHash).MaximumLength(40);
            RuleFor(expr => expr.LastAccessDate).NotEmpty();
            RuleFor(expr => expr.Location).MaximumLength(100);
            RuleFor(expr => expr.Reputation).NotEmpty();
            RuleFor(expr => expr.UpVotes).NotEmpty();
            RuleFor(expr => expr.Views).NotEmpty();
            RuleFor(expr => expr.WebsiteUrl).MaximumLength(200);
        }
    }

    public class VoteValidator : AbstractValidator<Vote>
    {
        public VoteValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.PostId).NotEmpty();
            RuleFor(expr => expr.VoteTypeId).NotEmpty();
            RuleFor(expr => expr.CreationDate).NotEmpty();
        }
    }

    public class VoteTypeValidator : AbstractValidator<VoteType>
    {
        public VoteTypeValidator()
        {
            RuleFor(expr => expr.Id).NotEmpty();
            RuleFor(expr => expr.Name).NotEmpty();
            RuleFor(expr => expr.Name).MaximumLength(50);
        }
    }

}
```
