firstattempt=true
userpath=""

echo ""
echo "finding bundle. . ."

userpath="../CommentBundle/"
currentdir=$(pwd)

cd ${userpath}Library/com.unity.addressables/aa/Windows/StandaloneWindows64/

bundle=$(find . -maxdepth 1 -name "*defaultlocalgroup*")
bundle=${bundle#./}

cd ${currentdir}

echo "copying unity bundle to project resources"
cp "${userpath}Library/com.unity.addressables/aa/Windows/StandaloneWindows64/${bundle}" "./resources/MauricePrime.bundle" 
echo "done! ! !"
