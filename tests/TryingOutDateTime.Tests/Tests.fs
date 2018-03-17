module Tests

open System
open Expecto
open TryingOutDateTime
open FSharpPlus
open System.Globalization
[<Tests>]
let tests =
  testList "TryParse" [
    testCase "DateTime kind" <| fun _ ->
      let dt =DateTime(2011,3,4,12,42,19)
      let expected = DateTimeKind.Unspecified
      Expect.equal dt.Kind expected "Manually initialized datetime should equal"
    testCase "DateTime.FSharpPlus" <| fun _ ->
      let v1 : DateTime       = parse "2011-03-04T15:42:19+03:00"
      let expected =DateTime(2011,3,4,12,42,19)
      Expect.equal v1 expected "Manually initialized datetime should equal"
    testCase "DateTimeOffset.FSharpPlus" <| fun _ ->
      let v2 : DateTimeOffset = parse "2011-03-04T15:42:19+03:00"
      let expected = DateTimeOffset(2011,3,4,15,42,19, TimeSpan.FromHours 3.)
      Expect.equal v2 expected "Manually initialized offset should equal"
    testCase "DateTime.FSharpPlus.Copy" <| fun _ ->
      let v1 = FromFsharpPlus.TryParseDateTime "2011-03-04T15:42:19+03:00"
      let expected =Some (DateTime(2011,3,4,12,42,19))
      Expect.equal v1 expected "Manually initialized datetime should equal"
    testCase "DateTimeOffset.FSharpPlus.Copy" <| fun _ ->
      let v2 = FromFsharpPlus.TryParseDateTimeOffset "2011-03-04T15:42:19+03:00"
      let expected = Some (DateTimeOffset(2011,3,4,15,42,19, TimeSpan.FromHours 3.))
      Expect.equal v2 expected "Manually initialized offset should equal"
  ]
