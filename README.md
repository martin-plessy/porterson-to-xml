# Introduction 

The exercise contains no tricks (though the last part is quite challenging and it is not unexpected if you cannot complete it perfectly).
The object of the exercise is to give you the opportunity to demonstrate your skills in a context that is as 'worklike' as possible.

You are encouraged to implement simple code, that shouldnt take a large ammount of time (again the last part is challenging, but is possible,
I suggest timeboxing any development you do for the last part to an hour or two, and present how far you have got, and what issues you think there are
not having a complete solution is by no means a failure - it took me an hour to write an working answer which I'm not actually very fond of - I know
how I'd write it next time!).
You are encouraged to ask questions as you would in a work environment, but there is no deliberate holes or tricks, I have tried to write the task so
that questions shouldnt be necessary, but like any work environment, even the best specifications are sometimes vague.

The aspects of your code should be, in this order:

Correctness
Maintainability
Simplicity
Confidence of correctness (testing and type checking)
Efficiency
Reusability

I would expect the answer to be in the form of a single solution with multiple console application projects. You can, in addition, have other projects
in your solution.
The code should be C#, and the focus is on the actual processing of the file, rather than nice to haves like logging or command line handling etc.

The requirements are:
i) develop a console application that transforms ExampleInput1.xml to ExampleOutput1.xml, where ExampleOutput1.xml is a normalised
transformation, where books are grouped together by author.
ii) develop another console application that transforms ExampleInput1.xml to ExampleOutput1.json.
Here the transformation is basically equivalent to the part i, except the output is in JSON.
iii) (challenging) - you should notice that the requirments for both the xml and json transformation are basically the same, except for the output format,
can you write an application where the transformation logic is written once, yet is capable of being used to transform the input into both file
formats. If you find this too hard, or are short of time, or you find your answers to the earlier parts arent easy to adapt to achieve this, then simply
give a sketch design of how you would try to achieve this.

# Getting Started

You should clone this repository to a folder on your machine, and you should be able to open the solution in visual studio.
You require dotnet 5.0 to run the example projects (feel free to downgrade the console apps to a framework you do have if you have to).
You should notice 3 console applications projects, one for each stage:

1) requirement i above corresponds to project PortersonToXml.
2) requirement ii above corresponds to project PortersonToJson.
3) requirement iii above corresponds to project PortersonToXmlAndJson.

The intention is we can see the application evolve as new requirements emerge, so don't 'upgrade' an earlier application to use the same approach as a later one,
the implementation of both 1 & 2 will be and should be simpler than the implementation of application 3.


# Build and Test

The applications should build in visual studio in the normal manner, and if you run the apps, they should error out with "not implemented" exceptions.

# Contribute

You should not try to push your answer back to this repository, but you should create your own repository somewhere else, or simply return your solution in a zip.