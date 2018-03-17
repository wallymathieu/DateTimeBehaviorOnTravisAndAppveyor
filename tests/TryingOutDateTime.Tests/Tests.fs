module Tests

open System
open Expecto
open TryingOutDateTime
open FSharpPlus
open System.Globalization
open Expecto.Logging
let inline assertEq expected actual description=
    Expect.isTrue (expected = actual) (sprintf "%s %A" description (expected, actual))
let assertDateTimeEqual expected actual=
    let dateTimeToString (d:DateTime) = d.ToString("yyyy-MM-ddTHH:mm:ssK", CultureInfo.InvariantCulture)
    assertEq (dateTimeToString expected) (dateTimeToString actual) "toString(yyyy-MM-ddTHH:mm:ssK)"

[<Tests>]
let tests =
  testList "TryParse" [
    testCase "DateTime kind" <| fun _ ->
      let dt =DateTime(2011,3,4,12,42,19)
      let expected = DateTimeKind.Unspecified
      Expect.equal dt.Kind expected "Manually initialized datetime should equal"
    testCase "DateTime.FSharpPlus" <| fun _ ->
      let v1 : DateTime       = parse "2011-03-04T15:42:19+03:00"
      let expected =DateTime(2011,3,4,12,42,19, DateTimeKind.Local)
      Expect.equal v1.Date expected.Date "Manually initialized datetime should equal (Date)"
      Expect.equal v1.Second expected.Second "Manually initialized datetime should equal (Second)"
      Expect.equal v1.Kind expected.Kind "Manually initialized datetime should equal (Kind)"
      printfn "\n\n\nfsharpplus)>>>>>>>>\n\n\n %A \n\n\n<<<<<<<\n\n\n" (v1.Hour, expected.Hour)
      assertDateTimeEqual expected v1
      //Expect.equal v1.Hour expected.Hour "Manually initialized datetime should equal (Hour)"
    testCase "DateTimeOffset.FSharpPlus" <| fun _ ->
      let v2 : DateTimeOffset = parse "2011-03-04T15:42:19+03:00"
      let expected = DateTimeOffset(2011,3,4,15,42,19, TimeSpan.FromHours 3.)
      Expect.equal v2 expected "Manually initialized offset should equal"
    testCase "DateTime.FSharpPlus.Copy" <| fun _ ->
      let v1 = FromFsharpPlus.TryParseDateTimeExact "2011-03-04T15:42:19+03:00"
      let expected = None//Some (DateTime(2011,3,4,12,42,19))
      Expect.equal v1 expected "Manually initialized datetime should equal"
    testCase "DateTime.TryParse" <| fun _ ->
      match FromFsharpPlus.TryParseDateTime "2011-03-04T15:42:19+03:00" with
      | Some v1->
         let expected = DateTime(2011,3,4,12,42,19, DateTimeKind.Local)
         Expect.equal v1.Date expected.Date "Manually initialized datetime should equal (Date)"
         Expect.equal v1.Second expected.Second "Manually initialized datetime should equal (Second)"
         Expect.equal v1.Kind expected.Kind "Manually initialized datetime should equal (Kind)"
         printfn "\n\n\nTryParse)>>>>>>>>\n\n\n %A \n\n\n<<<<<<<\n\n\n" (v1.Hour, expected.Hour)
         //Expect.equal v1.Hour expected.Hour "Manually initialized datetime should equal (Hour)"
         assertDateTimeEqual expected v1
      | None -> failwith "!"
    testCase "DateTimeOffset.FSharpPlus.Copy" <| fun _ ->
      let v2 = FromFsharpPlus.TryParseDateTimeOffsetExact "2011-03-04T15:42:19+03:00"
      let expected = Some (DateTimeOffset(2011,3,4,15,42,19, TimeSpan.FromHours 3.))
      Expect.equal v2 expected "Manually initialized offset should equal"
  ]
