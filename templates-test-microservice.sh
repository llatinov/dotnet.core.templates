#!/bin/sh

options=(true false)
dir_name=testProject
rm -rf $dir_name

for i in ${options[@]}
do
  for j in ${options[@]}
  do
    for k in ${options[@]}
    do
      for l in ${options[@]}
      do
        echo "Running: dotnet new microservice --ProjectName $dir_name --AddHealthChecks $i --AddSqsPublisher $j --AddSqsConsumer $k --AddSerilog $l"
        mkdir $dir_name
        cd $dir_name
        dotnet new microservice --ProjectName $dir_name --AddHealthChecks $i --AddSqsPublisher $j --AddSqsConsumer $k --AddSerilog $l > /dev/null
        dotnet build | grep 'error\|FAILED'
        dotnet test | grep 'error\|FAILED'
        cd ..
        rm -rf $dir_name
      done
    done
  done
done