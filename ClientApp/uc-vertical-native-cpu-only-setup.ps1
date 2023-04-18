# 10 Create the HorizontalPodAutoscaler
#See https://kubernetes.io/docs/tasks/run-application/horizontal-pod-autoscale-walkthrough/

write-host "Download vertical scaler source code"
cd "c:\temp"
git clone https://github.com/kubernetes/autoscaler.git





write-host "Deploy vertical scaler"
cd C:\D\msc_project\msc-project\sample-app
kubectl apply -f .\vpa.yaml