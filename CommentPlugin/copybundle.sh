firstattempt=true
userpath=""

currentdir=$(pwd)

rm ${currentdir}/resources/MauricePrime.bundle

echo ""
echo "finding bundle. . ."

userpath="../CommentBundle/"
currentdir=$(pwd)

cd ${userpath}Library/com.unity.addressables/aa/Windows/StandaloneWindows64/

bundle=$(find . -maxdepth 1 -name "*mauriceprime*")
bundle=${bundle#./}

cd ${currentdir}

echo "copying bundle to project resources"
cp "${userpath}Library/com.unity.addressables/aa/Windows/StandaloneWindows64/${bundle}" "./resources/MauricePrime.bundle"
echo "done! ! !"
