The Amazon S3 bucket below contains some .zip files

Inside each zip file is a CSV that maps the file name to a PO number (in a not very nice way - you'll have to do some parsing)

Extract all the PDF files from each zip file that hasn't been processed by your program before, and upload the PDF file back to the same S3 bucket with the path like: 'by-po/{po-number]/{original-file-name}.pdf'


Keep track of which Zip files have been processed, which PDF files came from which zip files, and what date the files were extracted (processing metadata).

Your program should remember which files were already extracted and only extract new files each time it runs.

The processing metadata should be stored in the same s3 bucket as everything else.


Other info:

The PO Number is typically a 9 or 10 digit number. You can find it on the PDFs under "Customer Order Number" typically. That can help you figure out parsing the info in the CSV. You won't be doing any OCR or anything like this in this project - just using the info in the CSV to map the files to PO numbers. (It will make more sense when you see the CSV)


Amazon S3 Bucket

Region: us-east-2

Bucket: xxxxxxxxxxx

Access Key ID: xxxxxxxxxxxxxxxxx

Secret: xxxxxxxxxxxxxxxxxxxxxxxx


Put together a console app in C# using .NET 7 that performs the functions above. Commit the code to a public GitHub repo you create, but exclude the S3 credentials from your code. Let me know if you have any questions, or need to be pointed in the right direction on anything. Approach this as if you were working at STAT already - reaching out for help is much better than spending days stuck in the same place. 

