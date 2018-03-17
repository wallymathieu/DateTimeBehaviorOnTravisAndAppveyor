namespace TryingOutDateTime
open System
open System.Globalization
open FSharpPlus.Control

module internal Prelude =
    let inline tupleToOption x = match x with true, value -> Some value | _ -> None
open Prelude
type FromFsharpPlus=

    static member TryParseDateTime = fun x -> DateTime.TryParse       (x, null, DateTimeStyles.RoundtripKind) |> tupleToOption : option<DateTime>

    static member TryParseDateTimeExact = fun x -> DateTime.TryParseExact       (x, [|"yyyy-MM-ddTHH:mm:ss.fffZ"; "yyyy-MM-ddTHH:mm:ssZ"|], null, DateTimeStyles.RoundtripKind) |> tupleToOption : option<DateTime>
    static member TryParseDateTimeOffsetExact = fun x -> DateTimeOffset.TryParseExact (x, [|"yyyy-MM-ddTHH:mm:ss.fffK"; "yyyy-MM-ddTHH:mm:ssK"|], null, DateTimeStyles.RoundtripKind) |> tupleToOption : option<DateTimeOffset>
