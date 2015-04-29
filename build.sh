
xbuild sys/sys.sln

rm -rf $1
mkdir $1
cp /sys/host/bin/Debug/* $1
