module CvWeb.Views.Interests

open CvWeb.Domain
open CvWeb.Content
open CvWeb.Views.Common

let view lang (items: Card list) =
    block "interests" lang Labels.interests (Skills.notes lang items)
