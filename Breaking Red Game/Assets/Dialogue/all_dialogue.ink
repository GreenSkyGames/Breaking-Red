EXTERNAL StartWolfHostility(bool)
EXTERNAL StopWolfHostility(bool)
EXTERNAL StartPurpleTorchEnemyHostility(bool)
EXTERNAL StopPurpleTorchEnemyHostility(bool)
EXTERNAL StartBearHostility(bool)
EXTERNAL StopBearHostility(bool)



-> TheWolf
-> PurpleTorchEnemy

VAR Name = "Default"

=== TheWolf ===
~ Name = "The Wolf"


Is that... Little Red Riding Hood?  In these woods again?  I thought you knew better than to come around here!

* [I should have known I'd find your mangy hide here, Wolf!]

* [What do you think you're doing here?!]

    I don't have to tell you anything.  Why so hostile?
    
    ** [I'm watching you, Wolf!]
        
        ~ StopWolfHostility(true)
        And what big eyes you have!
        
        *** [I'll be seeing you again!]
        
        -> END

- You've been gone from these woods for too long, Red!  You don't know how things work around here anymore!

    ** [I know enough about you!  You always had it in for Grandmother!]

    So what if I did?  What, did someone finally whack the old broad?

        *** [How dare you!  I'll kill you for that!]

        ~ StartWolfHostility(true)
        You can try, Red!  Show me what you got!
        
        **** [Let's go, dirtbag!]

        -> END


=== PurpleTorchEnemy ===
~ Name = "Purple Torch Enemy"

//Name is {Name}

testingtestingtesting

- -> END



=== TheBear ===
~ Name = "The Bear"

//Name is {Name}

Bear Testing Time!

* [Pain]

- He's BEARLY functional!

* [One more]

~ StartBearHostility(true)
- Can you BEAR it a little longer?


* [It's over]


- -> END