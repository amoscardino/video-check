
# Video Check

Command line tool for scanning video files for errors using ffmpeg and .NET 6.

## Requirements

- [ffmpeg](https://ffmpeg.org) (must be available from the command line)
- [.NET 6](https://dotnet.microsoft.com)

## Installation

Clone the repo to you computer. Pull to get updates. Open a command prompt to the source directory.

If you are updating, remove the old version first:

```cmd
> dotnet tool uninstall -g VideoCheck
```

To install:

```cmd
> dotnet build
> dotnet pack
> dotnet tool install -g --add-source ./dist VideoCheck
```

The tool will be available using the command `vcheck`.

## Usage

- `vcheck scan`
    - Scans the current directory (or input path) for video files and checks each file for errors.
        - Files that are scanned are kept in a log with their pass/fail status. The scan command will skip previously checked files.
    - Arguments:
        - `InputPath` - Path to scan. Defaults to current directory if no argument is provided.
            - Example: `vcheck scan /path/to/video/files` or `vcheck scan ../movies`.
    - Options:
        - `-r` or `--recursive` - If passed, subdirectories will be recursively scanned for files, not just the top level.
        - `-m` or `--minutes` - Number of minutes to check within each video file. Defaults to 2.
            - Note: more minutes will find more issues but will take longer to scan.
- `vcheck log`
    - Writes the log to the console. Only shows files that have failed a check before.
    - Options:
        - `-a` or `--all` - Show all files that have been scanned, not just errors.
- `vcheck log clear`
    - Deletes all records from the scan log.
- `vcheck log export`
    - Writes the log to an Excel file in the current directory (or output path)
    - Arguments:
        - `OutputPath` - Path where export file should be written. Defaults to current directory if no argument is provided.
            - Example: `vcheck log export /path/to/export` or `vcheck log export ..`.
    - Options:
        - `-a` or `--all` - Show all files that have been scanned, not just errors.
- All commands and subcommands support help options using `-h` or `--help`
- Version information can be found using `vcheck --version`

## Notes

Scanning is done by finding all video files (using the extensions: `mp4`, `mkv`, `m4v`, and `avi`) and running an ffmpeg command on them:

```bash
ffmpeg -v error -t <minutes * 60> -i <file path> -f null -
```

That command will read the first few minutes of a video, convert it to nothing, and discard the result. The catch is the verbosity is set to `error` so nothing will be output unless the ffmpeg cannot read the file (for whatever reason). If there is any output, that indicates a (possibly) corrupt file.

**This tool will only identify (possibly) corrupt videos but does not fix them.**

The scan log is kept in a [LiteDB](https://www.litedb.org/) file on disk at your user's local app data directory. This is `~\AppData\Local` on Windows and at `~/.local/share` on macOS/Linux.
