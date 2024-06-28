open FSharp.Data

[<Literal>]
let postsUrl = "https://jsonplaceholder.typicode.com/posts"

[<Literal>]
let commentsUrl = "https://jsonplaceholder.typicode.com/comments"

type Posts = JsonProvider<postsUrl>
type Comments = JsonProvider<commentsUrl>

let posts = Posts.Load(postsUrl)
let comments = Comments.Load(commentsUrl)

let lastPost id =
    query {
        for post in posts do
            where (post.Id = id)
            groupJoin comment in comments on (post.Id = comment.PostId) into comments

            select (
                post,
                query {
                    for comment in comments do
                        take 2
                        select comment
                }
            )

            exactlyOne
    }

printfn "%A" (lastPost 7)
