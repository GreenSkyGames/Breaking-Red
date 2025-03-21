EXTERNAL startWolfHostility(bool)
EXTERNAL stopWolfHostility(bool)
EXTERNAL startPurpleTorchEnemyHostility(bool)
EXTERNAL stopPurpleTorchEnemyHostility(bool)
EXTERNAL startBearHostility(bool)
EXTERNAL stopBearHostility(bool)



-> TheWolf
-> PurpleTorchEnemy

VAR Name = "Default"

=== TheWolf ===
~ Name = "The Wolf"


"Is that... Little Red Riding Hood?  In these woods again?  I thought you knew better than to come around here!"

* ["I should have known I'd find your mangy hide here, Wolf!"]

* ["What do you think you're doing here?!"]

    "I don't have to tell you anything.  Why so hostile?"
    
    ** ["I'm watching you, Wolf!"]
        
        ~ stopWolfHostility(true)
        "And what big eyes you have!"
        
        *** ["I'll be seeing you again!"]
        
        -> END

- "You've been gone from these woods for too long, Red!  You don't know how things work around here anymore!"

    ** ["I know enough about you!  You always had it in for Grandmother!"]

    "So what if I did?  What, did someone finally whack the old broad?"

        *** ["How dare you!  I'll kill you for that!"]

        ~ startWolfHostility(true)
        "You can try, Red!  Show me what you got!"
        
        **** ["Let's go, dirtbag!"]

        -> END


=== PurpleTorchEnemy ===
~ Name = "Purple Torch Enemy"

//Name is {Name}

testingtestingtestingLEAVE ME ALONEtestingtestingtesting

- -> END



=== TheBear ===
~ Name = "The Bear"

//Name is {Name}

- "..."

* ["Hey!  Hey, Bear!"]

- "Huh?  Who's that calling me?"

//bearApproach() if possible
* [Next]

- "Whoa!  It's Little Red Riding Hood!  You're back?"

* ["Don't play dumb with me, Bear!"]

- "Who's playin'?  It's been a long time, Red, surely you're not still mad?"

* ["Mad?  MAD?  I'm FURIOUS!"]

- "Whoa, what?  Just because of a little honey?  It's not like I killed anyone.  Uh.  Recently, that is."

* ["So WHO KILLED GRANDMOTHER?!"]

- "...Whoa.  The old bag finally kicked it?  Hah!  I thought the forest was busier lately!  I bet one of those humans got her!"

* ["If you didn't, then WHO?  TALK, Bear!"]

- "Huh, well, smart money is on the Hunter, if you ask me.  He's been killing all over the forest."

* [Next]

- "Then there's the Axman, but he just hates trees.  And there's that weird Hiker wandering around, and that chatty Hippie."

* [Next]

- "And you remember the Wizard.  He and Grandmother had a big fight a while back, really tore up the forest."

* ["The Hunter, the Axman, the Hiker, the Hippie, and the Wizard, huh.]

- "Yeah, bunch of weirdo humans.  Bet you anything it was one of them that did it."

* ["Why are you telling me so easily?"]

~ startBearHostility(true)
- "Well, it's not like you're going to get away from me AGAIN, is it?  Not too often a free meal comes to me!"

* ["Why you %$!@-!"]

- -> END



=== TheHiker ===
~ Name = "The Hiker"

//Name is {Name}

"My, at least it's a lovely day!"

* ["Hey!  You!  Stop right there!"]

- "Oh, hello there!  Another traveler!"

* ["Who are you?!  What are you doing in the forest?!"]

- "What?  Me?  Well, I went out looking for my Cat, but it's such a nice day out, I decided to go for a walk.  Have you seen The Cat anywhere?"

* ["Your cat?"]

- "Yes, he's a housecat and he got lost recently.  I've looked all over the forest for him, but no one has wanted to help.  I even talked to this old woman, and she... Well.  Let's just say that encounter didn't go very well!"

* ["Do you mean Grandmother?  What did you do to her?!"]

- "Hey now, I don't like that tone!  Look, if you're not going to help me find my Cat, then maybe just leave me alone!"

* [The Hiker refuses to speak further.]

- -> END




=== TheHippie ===
~ Name = "The Hippie"

//Name is {Name}

"What?  What's that red smudge I see... Oh hey!  Is that Little Red?"

* ["Long time no see."]

- "No kidding!  Wow, you've... Actually, you look exactly like I remember, now that I think about it."

* ["You, too.  Glad to see you're well.  Did you hear about Grandmother?"]

- "I did, man.  That's a real bummer.  I'm sorry for your loss, Red, even Grandmother didn't deserve to go out like that."

* ["Thanks.  I'm searching for who did it.  Would you have any leads?"]

* ["Wait, how did you know that?]

"Huh?  Know what?"

    ** ["How did you know that Grandmother was dead?  Who told you?"]
    
    "Huh... That's a good point!  Who did tell me!  Oh brother!"
    
        *** ["Try to rememeber!  It had to have been recently!"]
        
        "Oh!  Uh, then it had to be one of my friends!  They're around here if you go searching for them."
        
        **** [Next]
        
        "My friend The Fish is over by the lake, he sees all kinds of stuff.  And The Owl is off roosting in his tree, but you know how he is.  And there's this crazy Cat around.  If you ask anyone, ask him!"

        ***** ["The Fish, The Owl, and The Cat, huh.  Alright.  Thank you for helping."]

        "It's all our forest, man, we gotta stick together.  Stay safe out there, Red!  Lotta weirdos around the forest these days!"
        
        ****** ["Thanks again!"]
        
        -> END



- "Oh man.  I wish I could help, but I've just been here, doing my thing.  Maybe you can ask my friends?"

* [Next]

- "My friend The Fish is over by the lake, he sees all kinds of stuff.  And The Owl is off roosting in his tree, but you know how he is.  And there's this crazy Cat around.  If you ask anyone, ask him!"

* ["The Fish, The Owl, and The Cat, huh.  Alright.  Thank you for helping."]

- "It's all our forest, man, we gotta stick together.  Stay safe out there, Red!  Lotta weirdos around the forest these days!"

* ["You stay safe as well!"]

- -> END


