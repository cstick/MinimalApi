//using Web.Data;

//namespace Web.APIs.Groups;

//internal static partial class BatteryImageApis
//{
//    public static RouteGroupBuilder MapImageApi(this RouteGroupBuilder group)
//    {
//        group
//            .MapGet("{id}", async Task<IResult> (
//                Guid id,
//                IImageRepository images,
//                CancellationToken cancellationToken) =>
//            {
//                var image = await images.Get(id, cancellationToken);

//                if (image is null)
//                {
//                    return Results.NotFound("The image was not found.");
//                }

//                return Results.File(image.Content, image.ContentType, image.FileName);
//            })
//            .WithName(EndpointNames.Images.Get);

//        group.WithOpenApi();

//        return group;
//    }
//}