<?
$do = ($_POST['do']);

if($do == "send")
{
    $url       = "thanks.html";
    $recipient = "info@servicesolutionsco.com";
    $subject   = ($_POST['subject']);
    $name      = ($_POST['name']);
    $email     = ($_POST['email']);
    $phone     = ($_POST['phone']);
    $company   = ($_POST['company']);
    $message   = ($_POST['message']);
    $full_message   = "This email was sent from Service Solutions webform.\r\n Sender information:\r\n Name - " . $name . "\r\n E-mail - " . $email . "\r\n Phone - " . $phone . "\r\n Company - " . $company . "\r\n Original message:\r\n----------------------------------------\r\n" . $message ."\r\n----------------------------------------\r\n";
    $sendform  = mail("$recipient", "$subject", "$full_message", "From: $recipient (Service Solutions online request form)\r\nReply-to:$email");

    header("Location: $url");
}
?>