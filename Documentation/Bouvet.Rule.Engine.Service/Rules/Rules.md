# Documentation for rules used in rules engine

## CV not null

The CV object is not null.

## CV updated regularily

The CV should be updated at least once every 90 days.

## Valid email

The CV should be updated at least once every 90 days and email regular expression matches names with the following format:

- Starts with one or more characters that are not "@" or whitespace.
- Includes "@" symbol.
- Followed by one or more characters that are not "@" or whitespace.
- Includes a dot (.) symbol.
- Ends with one or more characters that are not "@" or whitespace.

## Valid employee name

This regular expression for employee name matches names with the following format:

- Is not null or empty.
- Starts with a capital letter.
- Includes any number of letters used in European countries, apostrophes, and hyphens between any two words.
- Each word must be at least two letters long and starts with a capital letter.
- Allows for a dot (.) at the end of the name.
- Only allows one whitespace between words.
