#!/bin/sh -l

set -eu

begin_cmd="/dotnet-sonarscanner begin"
end_cmd="/dotnet-sonarscanner end"

if [ -n "$INPUT_PROJECTKEY" ]
then
    begin_cmd="$begin_cmd /k:\"$INPUT_PROJECTKEY\""
fi

if [ -n "$INPUT_PROJECTNAME" ]
then
    begin_cmd="$begin_cmd /n:\"$INPUT_PROJECTNAME\""
fi

if [ -n "$INPUT_SONARHOSTNAME" ]
then
    begin_cmd="$begin_cmd /d:sonar.host.url=\"$INPUT_SONARHOSTNAME\""
fi

if [ -n "$INPUT_SONARORGANISATION" ]
then
    begin_cmd="$begin_cmd /o:\"$INPUT_SONARORGANISATION\""
fi

if [ -n "$INPUT_BEGINARGUMENTS" ]
then
    begin_cmd="$begin_cmd $INPUT_BEGINARGUMENTS"
fi

if [ -n "$INPUT_ENDARGUMENTS" ]
then
   end_cmd="$end_cmd $INPUT_ENDARGUMENTS"
fi

if [ -n "${SONAR_TOKEN}" ]
then
    begin_cmd="$begin_cmd /d:sonar.login=\"${SONAR_TOKEN}\""
    end_cmd="$end_cmd /d:sonar.login=\"${SONAR_TOKEN}\""
fi

sh -c "$begin_cmd"

if [ -n "$INPUT_BUILDCOMMAND" ]
then
    sh -c "$INPUT_BUILDCOMMAND"
fi

if [ -n "$INPUT_TESTCOMMAND" ]
then
    sh -c "$INPUT_TESTCOMMAND"
fi

sh -c "$end_cmd"
