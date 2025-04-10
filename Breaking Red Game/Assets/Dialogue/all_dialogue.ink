EXTERNAL startWolfHostility(bool)
EXTERNAL stopWolfHostility(bool)
EXTERNAL startPurpleTorchEnemyHostility(bool)
EXTERNAL stopPurpleTorchEnemyHostility(bool)
EXTERNAL startBearHostility(bool)
EXTERNAL stopBearHostility(bool)
EXTERNAL startWizardHostility(bool)
EXTERNAL stopWizardHostility(bool)
EXTERNAL startHunterHostility(bool)
EXTERNAL stopHunterHostility(bool)
EXTERNAL startAxmanHostility(bool)
EXTERNAL stopAxmanHostility(bool)
EXTERNAL addHunterClue(bool)
EXTERNAL addHikerClue(bool)
EXTERNAL addHippieClue(bool)
EXTERNAL addWizardClue(bool)
EXTERNAL addBearClue(bool)
EXTERNAL addFishClue(bool)
EXTERNAL addAxmanClue(bool)
EXTERNAL addCatClue(bool)
EXTERNAL checkAxmanClues(bool)
EXTERNAL checkCatCollectibles(bool)


//This are diverts to the various knots.
//Each knot represents the dialogue for one NPC.
-> TheWolf
-> PurpleTorchEnemy
-> TheHippie
-> TheHiker
-> TheBear
-> TheWizard
-> TheAxman
-> TheCat
-> TheFish
-> TheOwl
-> TheHunter

//This variable appears on the Name box beside the dialogue box.

VAR Name = "Default"


//This is the general template for NPC knots.
=== PurpleTorchEnemy ===
~ Name = "Purple Torch Enemy"

//Name is {Name}

testingtestingtestingLEAVE ME ALONEtestingtestingtesting

-> END


//TheWolf dialogue demonstrates a simple choice
//that results in hostility being activated or deactivated.
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

    ** ["I know enough! You always had it in for Grandmother!"]

    "So what if I did?  What, did someone finally whack the old broad?"

        *** ["How dare you!  I'll kill you for that!"]

        ~ startWolfHostility(true)
        "You can try, Red!  Show me what you got!"
        
        **** ["Let's go, dirtbag!"]

        -> END



//The Bear's dialogue was mangled during testing.
//
//This is what has to be done to not use the - symbol
//to separate the choices.
=== TheBear ===
~ Name = "The Bear"

//Name is {Name}

"..."

* ["Hey!  Hey, Bear!"]

"Huh?  Who's that calling me?"

//bearApproach() if possible
** [Next]

"Whoa!  It's Little Red Riding Hood!  You're back?"

*** ["Don't play dumb with me, Bear!"]

"Who's playin'?  It's been a long time, Red, surely you're not still mad?"

**** ["Mad?  MAD?  I'm FURIOUS!"]

"Whoa, what?  Just because of a little honey?  It's not like I killed anyone.  Uh.  Recently, that is."

***** ["So WHO KILLED GRANDMOTHER?!"]

"...Whoa.  The old bag finally kicked it?  Hah!  I thought the forest was busier lately!  I bet one of those humans got her!"

****** ["If you didn't, then WHO?  TALK, Bear!"]

"Huh, well, smart money is on the Hunter, if you ask me.  He's been killing all over the forest."

******* [Next]

"Then there's the Axman, but he just hates trees.  And there's that weird Hiker wandering around, and that chatty Hippie."

******** [Next]

"And you remember the Wizard.  He and Grandmother had a big fight a while back, really tore up the forest."

********* ["The Hunter, Axman, Hiker, Hippie, and Wizard, huh.]

~ addBearClue(true)
"Yeah, bunch of weirdo humans.  Bet you anything it was one of them that did it."

********** ["Why are you telling me so easily?"]


~ startBearHostility(true)

"Well, it's not like you're going to get away from me AGAIN, is it?  Not too often a free meal comes to me!"

*********** ["Why you %$!@-!"]

-> END



=== TheHiker ===
~ Name = "The Hiker"

//Name is {Name}

"My, at least it's a lovely day!"

* ["Hey!  You!  Stop right there!"]

- "Oh, hello there!  Another traveler!"

* ["Who are you?!  What are you doing in the forest?!"]

- "What?  Me?  Well, I went out looking for my Cat, but it's such a nice day out, I decided to go for a walk.  Have you seen The Cat anywhere?"

* ["Your cat?"]

- "Yes, he's a housecat and he got lost recently.  I've looked all over the forest for him!  I even talked to this old woman, and she... Well.  Let's just say that encounter didn't go very well!"


* ["Do you mean Grandmother?  What did you do to her?!"]

~addHikerClue(true)

- "Hey now, I don't like that tone!  Look, if you're not going to help me find my Cat, then maybe just leave me alone!"

* [The Hiker refuses to speak further.]

-> END




=== TheHippie ===
~ Name = "The Hippie"

//Name is {Name}

"What?  What's that red smudge I see... Oh hey!  Is that Little Red?"

* ["Long time no see."]

- "No kidding!  Wow, you've... Actually, you look exactly like I remember, now that I think about it."

* ["Glad to see you're well.  Did you hear about Grandmother?"]

- "I did, man.  That's a real bummer.  I'm sorry for your loss, Red, even Grandmother didn't deserve to go out like that."

* ["I'm searching for who did it.  Would you have any leads?"]

* ["Wait, how did you know that?]

"Huh?  Know what?"

    ** ["How did you know she was dead?  Who told you?"]
    
    "Huh... That's a good point!  Who did tell me!  Oh brother!  They said the guy did it with a weapon, too!  Crazy!"
    
        *** ["Try to rememeber!  It had to have been recently!"]
        
        "Oh!  Uh, then it had to be one of my friends!  They're around here if you go searching for them."
        
        **** [Next]

        ~addHippieClue(true)
        
        "My friend The Fish is over by the lake, he sees all kinds of stuff.  And The Owl is off roosting in his tree, but you know how he is.  And there's this crazy Cat around.  If you ask anyone, ask him!"

        ***** ["The Fish, The Owl, and The Cat.  Ok.  Thank you."]

        "It's all our forest, man, we gotta stick together.  Stay safe out there, Red!  Lotta weirdos around the forest these days!"
        
        ****** ["Thanks again!"]
        
        -> END



- "Oh man.  I wish I could help, but I've just been here, doing my thing.  Maybe you can ask my friends?"

* [Next]

- "My friend The Fish is over by the lake, he sees all kinds of stuff.  And The Owl is off roosting in his tree, but you know how he is.  And there's this crazy Cat around.  If you ask anyone, ask him!"

* ["The Fish, The Owl, and The Cat.  Ok.  Thank you."]

~addHippieClue(true)

- "It's all our forest, man, we gotta stick together.  Stay safe out there, Red!  Lotta weirdos around the forest these days!"

* ["You stay safe as well!"]

-> END



=== TheAxman ===
~ Name = "The Axman"

//Name is {Name}

{ checkAxmanClues(true):
    -> enough_clues_found
}

"Dum dee dum dee dum, swinging my ax, chopping the wood, dum dee dum dee dum...!  ...Oh? Can I help you with something?"

*["Maybe.  I'm investigating a murder in these woods."]

- "A murder?  Oh my goodness!  How terrible!  Do you have any leads?"

*["I'm working the case.  Have you see or heard anything?"]

- "Oh no, of course not!  All I ever see are these trees that need to be chopped down!  There are houses that need heating!"

*["Understandable.  If I have further questions, I'll come back."]

~ addAxmanClue(true)

- "I'm sure this is where I'll be.  These are my woods, after all!  Hohoho...!"

*[End]


-> END


=== enough_clues_found ===

"...Dum dee... dum..."

*["You.  It was YOU, wasn't it?!  TELL ME!"]

- "...You already know, don't you?  I can tell."

*["I know enough.  Why?!  Why did you do it?!"]

- "..."

*[Next]

- "You... stupid girl!  You think this is a GAME?!  You think this forest is all for FUN?!  You and your Grandmother!  Always getting in the way!  Telling us what we can and can't do!"

*[Next]

- "Well, these aren't YOUR trees, Red!  Thes are MY TREES, RED!  And I told that to Grandmother!  And now!  I'm going to tell you too!"

*["We'll see about that!"]

~startAxmanHostility(true)

- "I'll KILL you!!!"

*[Fight!]


-> END



=== TheWizard ===
~ Name = "The Wizard"

"Well, well.  Little Red Riding Hood.  You've come a long way to see me."

* ["What happened between you and Grandmother?"]

- "No time to catch up, eh?  You've been gone from the forest for years."

* ["Just tell me what happened, Wizard."]

- "I don't see what business it is of yours, Red.  You left the forest."]

* ["It's my business since Grandmother was murdered."]

- "...W-What?  Murdered?!"

* ["Like you don't know?  You have a crystal ball!"]

- "This... That's not...  Red, I think you should go.  I... I would rather be alone right now."

*["Fine.  But at least tell me what you know."]
        
*["Fat chance.  Tell me what happened.  Right now."] 

"I will tell you nothing with that tone, Red.  I have no reason to help you now."

    ** ["You think I'm just going to leave?"]
    
    "I will not help you, Red.  Leave me be."
    
        *** ["...Fine.  But if I find out you had something to do with it, I'm coming back!"]
        
        -> END
  

- "I cannot tell you what I do not know, Red.  This... is the first I am hearing of this."

* ["Then what CAN you do?"]

- "...Fine, I understand what you ask.  I shall use my powers to search for answers."]

* ["Thank you."]

- "I look... into the mists... I see..."

* [Next]

- "I see..."

* [Next]

- "I see..."

* [Next]

- "I see..."

* [Next]

- "I see..."

* [Next]

- "I see..."

* ["WHAT DO YOU SEE ALREADY"]

- "I see... a man!  A human man!  He wields a weapon of destruction!  There, in Grandmother's house, he struck!"
        
~ addWizardClue(true)

* [The Wizard slumps.]

- "That's all I saw."

* ["That's it?"]

* ["That's better than nothing.  Thank you."]

"It was... all I could do, Red.  Good luck.  And stay safe."

    ** [End]
    
    -> END
    

- "...Leave me be, Red.  This is not your forest anymore."

* ["I'm not leaving."]

- "I'm not asking, Red.  Leave.  Or I will make you."

* ["You think you have what it takes, old man?  Let's go!"]

~ startWizardHostility(true)

- "Impertinent fool!"

* [Fight]


-> END


=== TheCat ===
~ Name = "The Cat"


{ checkCatCollectibles(true):
    -> food_delivered
}


//Name is {The Cat}

"...Where did that Owl go... It was just there..."

* ["Hey, Cat!  What are you doing here?"]

- "Ugh, ANOTHER human.  What do YOU want?"

* ["There's been a murder in this forest."]

- "What, another one?"

* ["Another one?  What do you know?"]

- "Whoa, hold up there, Human!  What I know isn't free.  What are you going to give me, if you want my help?"

* ["What do you want?"]

* ["How about I give you a knuckle sandwich?"]

    "How rude!"
    
    ** [The offended Cat turns away and refuses to speak further.]
    
        -> END


- "Well, since you're being so generous and offering, why don't you get me something to eat?  There was a juicy Fish right around here, and I'm partial to a bit of Owl, too!"

* ["...I'll consider it."]

-> END


=== food_delivered ===

"Ooh, you brought the good stuff!  I can smell it!  Give it here!"

*[Sounds of crunching]

- "Mm, oh yeah, that's the stuff.  So do you want to like, scratch my back or something?"

*["I want to know who murdered Grandmother."]


- "Oh, right.  That.  The old woman who got the chop."

*["Tell me what you know!"]

- "Fine, fine, but only because that was such a tasty treat.  It was another human, an ugly male with a red bandana, carrying an axe."

*["Do you mean the Axman?"]

- "Sure, whatever.  He was pretty happy afterwards, humming and singing to himself when he walked off into the forest.

* ["I'm going to kill him!"]

~ addCatClue(true)

- "Alrighty, you do that then.  I'm gonna stay here, in front of this fire..."

* [End]

-> END



=== TheFish ===
~ Name = "The Fish"

//Name is {The Fish}

"Boy, this water is great!  I sure do love being alive!"

* ["Hey, Fish!  Stop right there!"]

- "Hello, Human!  You're not going to kill me, are you?"

* ["Kill you?  Why would I?"]

* ["Yeah, I might."]

    "Oh... Please don't?"
    
    ** ["You should have thought of that before being born a fish."]
    
        "Oh don't I wish!  Yipes!"
        
            *** [End]
        
            //Fish run away, if possible
        
            -> END


- "Well, the last human with a stick I saw was all covered in blood, and I think they probably just killed someone.  So I got worried when I saw you!"

* ["You saw a human with a stick, covered in blood?"]

- "I saw a human with a brown thing, pointy thing in their hands!  Probably a human...  Are you going to kill me?"

* ["Only if I need to."]

~addFishClue(true)

- "Well, that doesn't make me feel very good!"

* [End]

-> END


=== TheOwl ===
~ Name = "The Owl"

//Name is {The Hunter}

"Oh my stars above!  If it isn't Little Red Riding Hood!  WhatEVER brings you beneath my tree of our glorious forest once more?"

* ["I'm not here to chat, Owl."]

* ["I need information, Owl."]

"Oh my lands afar!  Well, I would just be tickled pink to help you out!  Whatever can I do?"

    ** ["Owl, someone killed Grandmother.  Have you seen anyone suspicious?"]
    
    "KILLED Grandmother, you say?  Oh my poor heart, suddenly so stricken!  WHOever could have done such a thing?"]
            
            *** ["Do you know something, Owl?!"]
            
                "I know many things, Little Red!  I am a wise and knowledgeable old owl after all!  All the secrets of the forest are mine!"
                
                    *** ["Then you better start talking, Owl!"]
                    
                        "Talking?  What of, Little Red?  There are so many topics to choose from!"
                        
                            **** ["Wha- About Grandmother!  Tell me who killed her!"]
                            
                                "Oh my words that flow from mine own tongue!  Perhaps your search for who killed her is but a search for the death of your own soul?"
                                
                                    ***** ["Owl, talk, or I start swinging."]
                                    
                                        "Upon this path of vengeance, would you truly slay the innocent?  How horrible!  What incentive have I to speak my mind now?"
                                        
                                            ****** ["I'm going to turn you into cat food, Owl.  Hold still!"]
                                            
                                            -> END
                                            

- "We are all but spirited children of our mother forest, aren't we, Red?  You know I do so love to use the glorious gift of gab!"

    ** ["I am aware.  Someone killed Grandmother, Owl."]
                            
        "Oh my words that flow from mine own tongue!  Perhaps your search for who killed her is but a search for the death of your own soul?"
            
            *** ["I see you haven't changed, Owl."]
                
                "My friend and dearest ally, Little Red Riding Hood, most fearsome of all the predators of the forest!  Whatever do you meant?"
                
                    **** ["Do you know anything or not, Owl?"]
                    
                        "You underestimate me dear Red!  I know so many, many things!  How could I ever reply to such a question so easily as the sands of time flow?"
                        
                            ***** ["I swear to God, Owl, if you know something..."]
                            
                                "You speak to a genius, dear Red!  Of COURSE I know things!  It is YOU who is not being specific!"
                                
                                ****** ["Not being... You know what, never mind."]
                                
-> END


=== TheHunter ===
~ Name = "The Hunter"

//Name is {The Hunter}

"AGH, I just HATE animals!  WHY are they still ALIVE?!"

* ["Uh, you okay there?"]

- "Oh great, look who it is!  Everyone's favorite treehugger, Little Red Riding Hood!"

* ["What are you doing in these woods?"]

* ["Who are you?"]

"What does it matter to YOU?  These forests are open to everyone that knows how to jump!"

    ** ["It matters a lot!  There's blood everywhere!"]
    
    "What are you some kind of detective now, as well as the big big forest girl?"
    
        *** ["I think you should start talking."]
        
        "Eat my shorts."
        
            **** ["...Really?"]
            
            -> END


- "What does it LOOK like?  These ANIMALS are just running around EVERYWHERE!  Being ALIVE!  Someone has to stop them!"

* ["That's... insane.  Do you need help of some kind?"]

- "SCREW YOU, Red!  I don't need help from you!  I didn't need help from Grandmother neither!"

* ["Grandmother?  What?  Do you know something?"]

- "You and her can both jump off a cliff, that's what I know!  That old biddy, putting up "No Hunting" signs all over the forest!  Whose side does she think she's on?"

* ["..."]

- "Not like the Wizard or that worthless Hippie can save us from the animals!  If all of you lazy tree-huggers weren't in the way, then I could finally kill ALL the animals!"

* ["Just calm down!"]

* ["Obviously I'm not going to let you do that!"]

    "What are you going to do to stop me?  I don't hunt humans, just like I told the Axman!"
    
        ** ["The Axman?  What did he say?"]
        
            "No!  I ain't saying another word!  Why don't you buzz off already, like a fly off these corpses!"
            
                *** [The Hunter refuses to speak further.]
                
                ~ addHunterClue(true)
                
                    -> END


- "I'll show you calm!"

* [End]
                
~ addHunterClue(true)
~ startHunterHostility(true)

-> END


