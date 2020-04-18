param($target="default")

. .\build-helpers

# $ build
#     Optimized for local development: updates databases instead of rebuilding them.
#
# $ build rebuild
#     Builds a clean local copy, rebuilding databases instead of updating them.

main {
    $targetFramework = "netcoreapp3.1"
    $configuration = 'Debug'

    $connectionStrings = @{
        DEV = connection-string MovieSessions MovieSessions/appsettings.Development.json;
        TEST = connection-string MovieSessions MovieSessions.Tests/appsettings.json
    }

    task ".NET SDK" { dotnet --version }
    task "Clean" { dotnet clean --configuration $configuration --nologo -v minimal } .
    task "Restore (Database Migration)" { dotnet restore --packages ./packages/ } ./MovieSessions.DatabaseMigration
    task "Restore (Solution)" { dotnet restore } .
    task "Build" { dotnet build --configuration $configuration --no-restore --nologo } .

    $devdb = gci .\MovieSessions\*.db | select -index 0 | select -ExpandProperty FullName
    $testdb = gci .\MovieSessions.Tests\*.db | select -index 0 | select -ExpandProperty FullName

    if ($target -eq "default") {
        task "Copy DEV db to Migrations for update" { copy-item $devdb -Destination .\MovieSessions.DatabaseMigration } .\
        task "Copy TEST db to Migrations for update" { copy-item $Testdb -Destination .\MovieSessions.DatabaseMigration } .\
        task "Update DEV/TEST Databases" { update-database DEV TEST } .\MovieSessions.DatabaseMigration
        
    } elseif ($target -eq "rebuild") {
        task "Remove DEV" { remove-item -path .\MovieSessions.DatabaseMigration\$((gci $devdb).Name) } .\
        task "Remove TEST" { remove-item -path .\MovieSessions.DatabaseMigration\$((gci $testdb).Name) } .\
        task "Update DEV/TEST Databases" { update-database DEV TEST } .\MovieSessions.DatabaseMigration
    }
    task "Copy DEV db" { copy-item .\MovieSessions.DatabaseMigration\$((gci $devdb).Name) -Destination $devdb -Force } .\
    task "Copy TEST db" { copy-item .\MovieSessions.DatabaseMigration\$((gci $testdb).Name) -Destination $testdb -Force } .\


    task "Test" { dotnet fixie --configuration $configuration --no-build } ./MovieSessions.Tests
}